using System;
using MongoDB.Driver;

namespace ConfigurationProject.ConfigurationLibrary
{
    public class ConfigurationReader
    {
        private readonly MongoDBConfigurationContext _mongoContext;
        private readonly RedisCacheService _redisCacheService;

        public ConfigurationReader(MongoDBConfigurationContext mongoContext, RedisCacheService redisCacheService)
        {
            _mongoContext = mongoContext;
            _redisCacheService = redisCacheService;
        }

        public T GetValue<T>(string key)
        {
            // Redis Cache kontrol edilir
            var cacheValue = _redisCacheService.GetCache(key);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return (T)Convert.ChangeType(cacheValue, typeof(T));
            }

            // Cache'de yoksa MongoDB'den alınır
            var configItem = _mongoContext.ConfigurationItems
                .Find(c => c.Name == key && c.IsActive)
                .FirstOrDefault();

            if (configItem != null)
            {
                // MongoDB'den alınan veri Redis'e cache'lenir
                _redisCacheService.SetCache(key, configItem.Value);
                return (T)Convert.ChangeType(configItem.Value, typeof(T));
            }

            return default;
        }

        public void SubscribeToConfigurationChanges()
        {
            _redisCacheService.SubscribeToChannel("config-updates", (channel, message) =>
            {
                // Config güncellemeleri burada işlenir
                Console.WriteLine($"Configuration updated: {message}");
            });
        }

        public void NotifyConfigurationChange(string key)
        {
            _redisCacheService.PublishMessage("config-updates", key);
        }

        public T GetValueWithFallback<T>(string key)
        {
            var cacheValue = _redisCacheService.GetCache(key);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return (T)Convert.ChangeType(cacheValue, typeof(T));
            }

            try
            {
                var configItem = _mongoContext.ConfigurationItems
                    .Find(c => c.Name == key && c.IsActive)
                    .FirstOrDefault();

                if (configItem != null)
                {
                    _redisCacheService.SetCache(key, configItem.Value);
                    return (T)Convert.ChangeType(configItem.Value, typeof(T));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                if (!string.IsNullOrEmpty(cacheValue))
                {
                    return (T)Convert.ChangeType(cacheValue, typeof(T));
                }
            }

            return default;
        }
    }
}

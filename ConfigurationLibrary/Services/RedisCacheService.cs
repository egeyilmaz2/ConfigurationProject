using StackExchange.Redis;

namespace ConfigurationProject.ConfigurationLibrary.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _cache;

        public RedisCacheService(string connectionString)
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            _cache = connectionMultiplexer.GetDatabase();
        }

        public string? GetCacheValue(string key)
        {
            return _cache.StringGet(key);
        }

        public void SetCacheValue(string key, string value)
        {
            _cache.StringSet(key, value);
        }

        public void SetCacheValue(string key, string value, TimeSpan expiration)
        {
            _cache.StringSet(key, value, expiration);
        }

        public void RemoveCacheValue(string key)
        {
            _cache.KeyDelete(key);
        }
    }
}

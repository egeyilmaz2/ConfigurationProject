using StackExchange.Redis;

namespace ConfigurationLibrary
{
    public class RedisCacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCacheService(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _database = _redis.GetDatabase();
        }

        public void SetCache(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public string GetCache(string key)
        {
            return _database.StringGet(key);
        }

        public void PublishMessage(string channel, string message)
        {
            _redis.GetSubscriber().Publish(channel, message);
        }

        public void SubscribeToChannel(string channel, Action<RedisChannel, RedisValue> handler)
        {
            _redis.GetSubscriber().Subscribe(channel, handler);
        }
    }
}

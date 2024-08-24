using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ConfigurationProject.ConfigurationLibrary
{
    public class MongoDBConfigurationContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBConfigurationContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<ConfigurationItem> ConfigurationItems =>
            _database.GetCollection<ConfigurationItem>("ConfigurationItems");
    }
}

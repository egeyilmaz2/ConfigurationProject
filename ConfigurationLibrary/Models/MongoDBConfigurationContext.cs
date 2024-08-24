using MongoDB.Driver;
using ConfigurationProject.ConfigurationLibrary.Models;

namespace ConfigurationProject.ConfigurationLibrary
{
    public class MongoDBConfigurationContext
    {
        private readonly IMongoCollection<ConfigurationItem> _configurationItems;

        public MongoDBConfigurationContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _configurationItems = database.GetCollection<ConfigurationItem>("ConfigurationItems");
        }

        public List<ConfigurationItem> GetActiveConfigurationItems(string applicationName)
        {
            return _configurationItems.Find(item => item.ApplicationName == applicationName && item.IsActive).ToList();
        }

        public ConfigurationItem GetConfigurationItemByName(string applicationName, string name)
        {
            return _configurationItems.Find(item => item.ApplicationName == applicationName && item.Name == name && item.IsActive).FirstOrDefault();
        }

        public void AddConfigurationItem(ConfigurationItem item)
        {
            _configurationItems.InsertOne(item);
        }

        public void UpdateConfigurationItem(string id, ConfigurationItem updatedItem)
        {
            _configurationItems.ReplaceOne(item => item.Id == id, updatedItem);
        }
    }
}

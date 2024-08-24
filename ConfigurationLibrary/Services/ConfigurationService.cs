using ConfigurationLibrary.Interfaces;

namespace ConfigurationLibrary.Services
{
    public class ConfigurationService
    {
        private readonly IConfigurationReader _configurationReader;

        public ConfigurationService(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        public T GetConfigurationValue<T>(string key)
        {
            return _configurationReader.GetValue<T>(key);
        }

        public void UpdateConfiguration(string key, string value)
        {
            // Konfigürasyon güncelleme mantığı
            _configurationReader.NotifyConfigurationChange(key);
        }
    }
}

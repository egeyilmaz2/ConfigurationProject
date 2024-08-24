namespace ConfigurationLibrary.Interfaces
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
        void SubscribeToConfigurationChanges();
        void NotifyConfigurationChange(string key);
    }
}

using Microsoft.AspNetCore.Mvc;
using ConfigurationProject.ConfigurationLibrary;


namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationReader _configurationReader;

        public ConfigurationController(ConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        [HttpGet("{key}")]
        public IActionResult GetConfigurationValue(string key)
        {
            var value = _configurationReader.GetValue<string>(key);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }
    }
}

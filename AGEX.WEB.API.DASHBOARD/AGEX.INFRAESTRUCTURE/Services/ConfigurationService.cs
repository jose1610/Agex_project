using AGEX.CORE.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace AGEX.INFRAESTRUCTURE.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Get<T>(string section) => _configuration.GetSection(section).Get<T>();
    }
}

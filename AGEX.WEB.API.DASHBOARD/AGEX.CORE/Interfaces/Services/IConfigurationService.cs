namespace AGEX.CORE.Interfaces.Services
{
    public interface IConfigurationService
    {
        T Get<T>(string section);
    }
}
using AGEX.CORE.Enumerations;

namespace AGEX.CORE.Interfaces.Services
{
    public interface ILogService
    {
        string GetRequestId();
        void SaveLogApp(string method, string message, LogType logType);
    }
}
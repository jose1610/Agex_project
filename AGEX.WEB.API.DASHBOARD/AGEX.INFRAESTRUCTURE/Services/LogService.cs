using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Services;
using AGEX.CORE.Models.Configuration;
using AGEX.CORE.Models.Log;
using AGEX.CORE.Services;
using System.Diagnostics;

namespace AGEX.INFRAESTRUCTURE.Services
{
    public class LogService : ILogService
    {
        private readonly ConfigurationLog _configurationLog;
        private readonly IParseService _parseService;
        private readonly string _requestId;
        private string _controller;

        public LogService(IConfigurationService configurationLog, IParseService parseService)
        {
            _configurationLog = configurationLog.Get<ConfigurationLog>(ConfigurationSection.LogService);
            _requestId = UtilService.GetId(32);
            _parseService = parseService;
        }

        public string GetRequestId()
        {
            return _requestId;
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void SaveLogApp(string method, string message, LogType logType)
        {
            string type = Enum.GetName(typeof(LogType), logType) ?? "";
            string path = $"{_configurationLog.Path}{ConfigurationLog.Date}\\{type}\\{DateTime.Now:HH}\\";

            LogModel logModel = new();
            try
            {
                logModel = new LogModel
                {
                    RequestId = _requestId,
                    Message = message,
                    Create_date = DateTime.Now.ToString("yyyy-MM-dd"),
                    Create_datetime = DateTime.Now.ToString("HH:mm:ss"),
                    Method = method
                };

                CreateDirectory(path);
                string line = $"[{DateTime.Now:dd/MM/yyyy} {DateTime.Now:HH:mm:ss fff} - {_requestId} - {_controller}]";
                line = $"{line}: {_parseService.Serialize(logModel)}{Environment.NewLine}";
                File.AppendAllText($"{path}{_configurationLog.NameFile}", line);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists(nameof(AGEX.CORE)))
                    EventLog.CreateEventSource(nameof(AGEX.CORE), "Log");

                EventLog elResource = new()
                {
                    Source = nameof(AGEX.CORE)
                };
                elResource.WriteEntry($"Error [{nameof(LogService)}, {nameof(SaveLogApp)}]: {ex.Message}", EventLogEntryType.Error, 0);
            }
        }
    }
}

using AGEX.CORE.Enumerations;
using AGEX.CORE.Exceptions;
using AGEX.CORE.Interfaces.Services;
using AGEX.CORE.Models.Configuration;
using AGEX.INFRAESTRUCTURE.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AGEX.INFRAESTRUCTURE.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly ConfigurationMessages _configurationMessages;

        public GlobalExceptionFilter(ILogService logService, IParseService parseService, IConfigurationService configurationService)
        {
            _logService = logService;
            _parseService = parseService;
            _configurationMessages = configurationService.Get<ConfigurationMessages>(ConfigurationSection.MessagesDefault);
        }

        public void OnException(ExceptionContext context)
        {
            string message;
            if (context.Exception.GetType() == typeof(CustomException))
                message = context.Exception.Message;
            else if (context.Exception.GetType() == typeof(TimeoutException))
                message = _configurationMessages.TimeoutMessage;
            else
                message = _configurationMessages.FatalErrorMessage;

            _logService.SaveLogApp($"{nameof(OnException)}", $"[RESPONSE][[{OnException}][{context.Exception.Source} - {_parseService.Serialize(context.Exception.Message)} - {context.Exception.StackTrace}]", LogType.Error);

            var json = new
            {
                Message = message
            };
            context.Result = new Microsoft.AspNetCore.Mvc.OkObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.ExceptionHandled = true;
        }
    }
}

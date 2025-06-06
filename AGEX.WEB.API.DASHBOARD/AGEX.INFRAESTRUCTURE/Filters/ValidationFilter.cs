using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Services;
using AGEX.CORE.Models;
using AGEX.CORE.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AGEX.INFRAESTRUCTURE.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidationFilter : Attribute, IAsyncActionFilter
    {
        private ILogService _logService;
        private IParseService _parseService;
        private IConfigurationService _configurationService;
        private readonly IActivityService _activityService;
        private object jsonBadRequest;
        private string invalidParameterMessage;

        public ValidationFilter(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public void InitService(ActionContext context)
        {
            _logService = context.HttpContext.RequestServices.GetService<ILogService>();
            _parseService = context.HttpContext.RequestServices.GetService<IParseService>();
            _configurationService = context.HttpContext.RequestServices.GetService<IConfigurationService>();

            invalidParameterMessage = _configurationService.Get<ConfigurationMessages>(ConfigurationSection.MessagesDefault).InvalidParameters;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            InitService(context);

            if (context.ActionArguments.ContainsKey("request"))
            {
                string requestString = _parseService.Serialize(context.ActionArguments["request"]);

                _logService.SaveLogApp($"[{nameof(OnActionExecutionAsync)}]", $"[REQUEST][{nameof(OnActionExecutionAsync)}][{requestString}]", LogType.Information);
                await _activityService.RegisterActivity(new ActivityModel
                {
                    ActivityId = _logService.GetRequestId(),
                    JsonRequest = requestString
                });

                if (!context.ModelState.IsValid)
                {
                    string message = string.Empty;
                    foreach (var modelStateKey in context.ModelState.Keys)
                    {
                        message += $"{modelStateKey}|";
                    }
                    BadRequestResult(context, "", $"Por favor verificar: {message.TrimEnd('|')}");
                    return;
                }
            }
            await next();
        }

        private void BadRequestResult(ActionExecutingContext context, string applicationId, string message = "")
        {
            jsonBadRequest = new
            {
                message = $"{invalidParameterMessage} - {message}"
            };

            context.Result = new OkObjectResult(jsonBadRequest);

            _logService.SaveLogApp($"[{nameof(BadRequestResult)}]", $"[RESPONSE][{nameof(BadRequestResult)}][{applicationId} {context.HttpContext.Request.Path.Value} : {invalidParameterMessage} - {message}]", LogType.Information);
            _activityService.RegisterActivity(new ActivityModel
            {
                ActivityId = _logService.GetRequestId(),
                JsonResponse = message
            });
        }
    }
}

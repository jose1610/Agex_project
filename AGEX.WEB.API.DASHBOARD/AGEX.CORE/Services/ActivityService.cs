using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Interfaces.Services;
using AGEX.CORE.Models;

namespace AGEX.CORE.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;

        public ActivityService(IActivityRepository activityRepository, ILogService logService, IParseService parseService)
        {
            _activityRepository = activityRepository;
            _logService = logService;
            _parseService = parseService;
        }

        public async Task<int> RegisterActivity(ActivityModel request)
        {
            _logService.SaveLogApp($"[{nameof(RegisterActivity)}]", $"[REQUEST][{nameof(RegisterActivity)}][{_parseService.Serialize(request)}]", LogType.Information);

            await _activityRepository.RegisterActivity(request);

            _logService.SaveLogApp($"[{nameof(RegisterActivity)}]", $"[RESPONSE][{nameof(RegisterActivity)}][SUCCESS: Activity: {request.ActivityId} - ResponseCode: {ResponseCode.Success}]", LogType.Information);

            return ResponseCode.Success;
        }

        public async Task<int> UpdateActivity(ActivityModel request)
        {
            _logService.SaveLogApp($"[{nameof(UpdateActivity)}]", $"[REQUEST][{nameof(UpdateActivity)}][{_parseService.Serialize(request)}]", LogType.Information);

            await _activityRepository.UpdateActivity(request);

            _logService.SaveLogApp($"[{nameof(UpdateActivity)}]", $"[RESPONSE][{nameof(UpdateActivity)}][SUCCESS: Activity: {request.ActivityId} - ResponseCode: {ResponseCode.Success}]", LogType.Information);

            return ResponseCode.Success;
        }
    }
}

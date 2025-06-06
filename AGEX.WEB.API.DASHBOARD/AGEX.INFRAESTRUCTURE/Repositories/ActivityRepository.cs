using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Models;

namespace AGEX.INFRAESTRUCTURE.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IBaseAgexRepository _baseRepository;
        private readonly string _sp = "sp_activity";

        public ActivityRepository(IBaseAgexRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> RegisterActivity(ActivityModel request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "REGISTER_ACTIVITY" },
                {"@i_activityId", request.ActivityId},
                {"@i_jsonRequest", request.JsonRequest },
                {"@i_type", "POST" }
            });

            return ResponseCode.Success;
        }

        public async Task<int> UpdateActivity(ActivityModel request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "UPDATE_ACTIVITY" },
                {"@i_activityId", request.ActivityId },
                {"@i_jsonResponse", request.JsonResponse }
            });

            return ResponseCode.Success;
        }
    }
}

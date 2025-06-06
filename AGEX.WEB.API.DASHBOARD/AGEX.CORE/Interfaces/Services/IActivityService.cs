using AGEX.CORE.Models;

namespace AGEX.CORE.Interfaces.Services
{
    public interface IActivityService
    {
        Task<int> RegisterActivity(ActivityModel request);
        Task<int> UpdateActivity(ActivityModel request);
    }
}
using AGEX.CORE.Models;

namespace AGEX.CORE.Interfaces.Repositories
{
    public interface IActivityRepository
    {
        Task<int> RegisterActivity(ActivityModel request);
        Task<int> UpdateActivity(ActivityModel request);
    }
}
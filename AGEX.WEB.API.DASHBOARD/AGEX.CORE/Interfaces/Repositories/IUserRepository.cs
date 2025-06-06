using AGEX.CORE.Dtos.Login.Get;
using AGEX.CORE.Dtos.Login.Register;
using AGEX.CORE.Models.User;

namespace AGEX.CORE.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<GetUsersResDto>> GetUser(GetUsersReqDto request);
        Task<UserAttemptsModel> GetUserPassword(RegisterUserReqDto request);
        Task<List<GetUsersResDto>> GetUsers(GetUsersReqDto request);
        Task<int> RegisterUser(RegisterUserReqDto request);
        Task<int> UpdateLastLogin(RegisterUserReqDto request);
        Task<int> UpdateUserAttempts(RegisterUserReqDto request);
        Task<int> UpdateUserPassword(RegisterUserReqDto request);
    }
}
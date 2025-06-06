using AGEX.CORE.Dtos.Login.Enter;
using AGEX.CORE.Dtos.Login.Get;
using AGEX.CORE.Dtos.Login.Register;

namespace AGEX.CORE.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<GetUsersResDto>> GetUser(GetUsersReqDto request);
        Task<LoginResDto> Login(LoginReqDto request);
        Task<RegisterUserResDto> RegisterUser(RegisterUserReqDto request);
    }
}
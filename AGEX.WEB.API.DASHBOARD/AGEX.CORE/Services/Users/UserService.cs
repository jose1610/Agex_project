using AGEX.CORE.Dtos.Login.Enter;
using AGEX.CORE.Dtos.Login.Get;
using AGEX.CORE.Dtos.Login.Register;
using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Repositories.Users;
using AGEX.CORE.Interfaces.Services;

namespace AGEX.CORE.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly ICryptoService _cryptoService;

        public UserService(ILogService logService, IParseService parseService, IUserRepository userRepository, ICryptoService cryptoService)
        {
            _logService = logService;
            _parseService = parseService;
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<LoginResDto> Login(LoginReqDto request)
        {
            _logService.SaveLogApp($"[{nameof(Login)}]", $"[REQUEST][[{nameof(Login)}{_parseService.Serialize(request)}]", LogType.Information);

            LoginResDto response = new();

            RegisterUserReqDto registerUser = new()
            {
                UserName = request.UserName
            };
            var passModel = await _userRepository.GetUserPassword(registerUser);

            var pass = _cryptoService.Decode(passModel.Password);

            if (pass.Equals(request.Password) && request.UserName.Equals(passModel.UserName))
            {
                response.message = "Success";
                await _userRepository.UpdateLastLogin(registerUser);
                return response;
            }
            else
            {
                await _userRepository.UpdateUserAttempts(registerUser);
                response.message = "Error en usuario o contraseña incorrecto";
            }

            _logService.SaveLogApp($"[{nameof(Login)}]", $"[REQUEST][[{nameof(Login)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<RegisterUserResDto> RegisterUser(RegisterUserReqDto request)
        {
            RegisterUserResDto response = new();

            _logService.SaveLogApp($"[{nameof(RegisterUser)}]", $"[REQUEST][[{nameof(RegisterUser)}{_parseService.Serialize(request)}]", LogType.Information);

            request.Password = "$" + _cryptoService.Encode(request.Password);

            var dt = await _userRepository.RegisterUser(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(RegisterUser)}]", $"[REQUEST][[{nameof(RegisterUser)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<List<GetUsersResDto>> GetUser(GetUsersReqDto request)
        {
            List<GetUsersResDto> response = new();

            _logService.SaveLogApp($"[{nameof(GetUser)}]", $"[REQUEST][[{nameof(GetUser)}{_parseService.Serialize(request)}]", LogType.Information);

            if (string.IsNullOrEmpty(request.UserName))
                response = await _userRepository.GetUsers(request);
            else
                response = await _userRepository.GetUser(request);


            _logService.SaveLogApp($"[{nameof(GetUser)}]", $"[REQUEST][[{nameof(GetUser)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }
    }
}

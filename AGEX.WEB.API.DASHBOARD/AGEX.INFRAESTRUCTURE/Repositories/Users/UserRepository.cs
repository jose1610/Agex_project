using AGEX.CORE.Dtos.Login.Get;
using AGEX.CORE.Dtos.Login.Register;
using AGEX.CORE.Enumerations;
using AGEX.CORE.Exceptions;
using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Interfaces.Repositories.Users;
using AGEX.CORE.Models.User;
using System.Data;

namespace AGEX.INFRAESTRUCTURE.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseAgexRepository _baseRepository;
        private readonly string _sp = "sp_user";

        public UserRepository(IBaseAgexRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> RegisterUser(RegisterUserReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "REGISTER_USER" },
                {"@i_user_id", request.UserName},
                {"@i_user_password", request.Password }
            });

            return ResponseCode.Success;
        }

        public async Task<int> UpdateUserPassword(RegisterUserReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "UPDATE_USER_PASSWORD" },
                {"@i_user_id", request.UserName},
                {"@i_user_password", request.Password }
            });

            return ResponseCode.Success;
        }

        public async Task<int> UpdateLastLogin(RegisterUserReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "UPDATE_USER_LAST_LOGIN" },
                {"@i_user_id", request.UserName}
            });

            return ResponseCode.Success;
        }
        public async Task<int> UpdateUserAttempts(RegisterUserReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "UPDATE_USER_ATTEMPTS" },
                {"@i_user_id", request.UserName}
            });

            return ResponseCode.Success;
        }

        public async Task<UserAttemptsModel> GetUserPassword(RegisterUserReqDto request)
        {
            UserAttemptsModel model = new();

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_USER_PASSWORD" },
                {"@i_user_id", request.UserName}
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetUserPassword)} (c).");

            model.UserName = dt.Rows[0]["USER_ID"].ToString(); ;
            model.Password = dt.Rows[0]["USER_PASSWORD"].ToString();
            model.Attempts = dt.Rows[0]["ATTEMPTS"].ToString();

            return model;
        }

        public async Task<List<GetUsersResDto>> GetUser(GetUsersReqDto request)
        {
            List<GetUsersResDto> userModels = [];

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_USER" },
                {"@i_user_id", request.UserName}
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetUser)} (c).");

            foreach (DataRow dr in dt.Rows)
            {
                userModels.Add(new GetUsersResDto
                {
                    UserName = dr["USER_ID"].ToString(),
                    Password = dr["USER_PASSWORD"].ToString(),
                    Attempts = dr["ATTEMPTS"].ToString(),
                    CreateDatetime = dr["CREATE_DATETIME"].ToString()
                });
            }

            return userModels;
        }

        public async Task<List<GetUsersResDto>> GetUsers(GetUsersReqDto request)
        {
            List<GetUsersResDto> userModels = [];

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_USERS" }
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetUsers)} (c).");

            foreach (DataRow dr in dt.Rows)
            {
                userModels.Add(new GetUsersResDto
                {
                    UserName = dr["USER_ID"].ToString(),
                    Password = dr["USER_PASSWORD"].ToString(),
                    Attempts = dr["ATTEMPTS"].ToString(),
                    CreateDatetime = dr["CREATE_DATETIME"].ToString()
                });
            }

            return userModels;
        }
    }
}

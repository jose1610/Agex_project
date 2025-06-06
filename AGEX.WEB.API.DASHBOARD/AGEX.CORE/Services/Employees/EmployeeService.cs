using AGEX.CORE.Dtos.Employees.Delete;
using AGEX.CORE.Dtos.Employees.Get;
using AGEX.CORE.Dtos.Employees.Register;
using AGEX.CORE.Dtos.Employees.Update;
using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Repositories.Employees;
using AGEX.CORE.Interfaces.Services;

namespace AGEX.CORE.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly ICryptoService _cryptoService;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogService logService, IParseService parseService, ICryptoService cryptoService)
        {
            _employeeRepository = employeeRepository;
            _logService = logService;
            _parseService = parseService;
            _cryptoService = cryptoService;
        }

        public async Task<RegisterEmployeeResDto> RegisterEmployee(RegisterEmployeeReqDto request)
        {
            RegisterEmployeeResDto response = new();

            _logService.SaveLogApp($"[{nameof(RegisterEmployee)}]", $"[REQUEST][[{nameof(RegisterEmployee)}{_parseService.Serialize(request)}]", LogType.Information);

            await _employeeRepository.RegisterEmployee(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(RegisterEmployee)}]", $"[REQUEST][[{nameof(RegisterEmployee)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<UpdateEmployeeResDto> UpdateEmployee(UpdateEmployeeReqDto request)
        {
            UpdateEmployeeResDto response = new();

            _logService.SaveLogApp($"[{nameof(UpdateEmployee)}]", $"[REQUEST][[{nameof(UpdateEmployee)}{_parseService.Serialize(request)}]", LogType.Information);

            await _employeeRepository.UpdateEmployee(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(UpdateEmployee)}]", $"[REQUEST][[{nameof(UpdateEmployee)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<List<GetEmployeeResDto>> GetEmployee(GetEmployeeReqDto request)
        {
            List<GetEmployeeResDto> response = new();

            _logService.SaveLogApp($"[{nameof(GetEmployee)}]", $"[REQUEST][[{nameof(GetEmployee)}{_parseService.Serialize(request)}]", LogType.Information);

            if (string.IsNullOrEmpty(request.DPI))
                response = await _employeeRepository.GetEmployees(request);
            else
                response = await _employeeRepository.GetEmployee(request);

            _logService.SaveLogApp($"[{nameof(GetEmployee)}]", $"[REQUEST][[{nameof(GetEmployee)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<DeleteEmployeeResDto> DeleteEmployee(DeleteEmployeeReqDto request)
        {
            DeleteEmployeeResDto response = new();

            _logService.SaveLogApp($"[{nameof(DeleteEmployee)}]", $"[REQUEST][[{nameof(DeleteEmployee)}{_parseService.Serialize(request)}]", LogType.Information);

            await _employeeRepository.DeleteEmployee(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(DeleteEmployee)}]", $"[REQUEST][[{nameof(DeleteEmployee)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }
    }
}

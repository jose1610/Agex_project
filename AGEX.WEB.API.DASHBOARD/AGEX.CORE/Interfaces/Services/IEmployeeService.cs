using AGEX.CORE.Dtos.Employees.Delete;
using AGEX.CORE.Dtos.Employees.Get;
using AGEX.CORE.Dtos.Employees.Register;
using AGEX.CORE.Dtos.Employees.Update;

namespace AGEX.CORE.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<DeleteEmployeeResDto> DeleteEmployee(DeleteEmployeeReqDto request);
        Task<List<GetEmployeeResDto>> GetEmployee(GetEmployeeReqDto request);
        Task<RegisterEmployeeResDto> RegisterEmployee(RegisterEmployeeReqDto request);
        Task<UpdateEmployeeResDto> UpdateEmployee(UpdateEmployeeReqDto request);
    }
}
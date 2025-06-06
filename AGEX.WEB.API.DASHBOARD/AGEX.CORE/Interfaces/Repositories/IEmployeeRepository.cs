using AGEX.CORE.Dtos.Employees.Delete;
using AGEX.CORE.Dtos.Employees.Get;
using AGEX.CORE.Dtos.Employees.Register;
using AGEX.CORE.Dtos.Employees.Update;
using AGEX.CORE.Models.Employees;

namespace AGEX.CORE.Interfaces.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Task<int> DeleteEmployee(DeleteEmployeeReqDto request);
        Task<List<GetEmployeeResDto>> GetEmployee(GetEmployeeReqDto request);
        Task<List<GetEmployeeResDto>> GetEmployees(GetEmployeeReqDto request);
        Task<int> RegisterEmployee(RegisterEmployeeReqDto request);
        Task<int> UpdateEmployee(UpdateEmployeeReqDto request);
    }
}
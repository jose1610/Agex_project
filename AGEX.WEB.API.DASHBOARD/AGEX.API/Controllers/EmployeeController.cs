using AGEX.CORE.Dtos.Employees.Delete;
using AGEX.CORE.Dtos.Employees.Get;
using AGEX.CORE.Dtos.Employees.Register;
using AGEX.CORE.Dtos.Employees.Update;
using AGEX.CORE.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AGEX.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route(nameof(RegisterEmployee)), HttpPost]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeReqDto request)
        {
            var dt = await _employeeService.RegisterEmployee(request);

            return Ok(dt);
        }

        [Route(nameof(UpdateEmployee)), HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeReqDto request)
        {
            var dt = await _employeeService.UpdateEmployee(request);

            return Ok(dt);
        }

        [Route(nameof(GetEmployee)), HttpPost]
        public async Task<IActionResult> GetEmployee([FromBody] GetEmployeeReqDto request)
        {
            var dt = await _employeeService.GetEmployee(request);

            return Ok(dt);
        }

        [Route(nameof(DeleteEmployee)), HttpPost]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployeeReqDto request)
        {
            var dt = await _employeeService.DeleteEmployee(request);

            return Ok(dt);
        }
    }
}

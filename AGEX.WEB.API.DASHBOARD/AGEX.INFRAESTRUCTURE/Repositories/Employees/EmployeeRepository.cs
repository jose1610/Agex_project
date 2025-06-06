using AGEX.CORE.Dtos.Employees.Delete;
using AGEX.CORE.Dtos.Employees.Get;
using AGEX.CORE.Dtos.Employees.Register;
using AGEX.CORE.Dtos.Employees.Update;
using AGEX.CORE.Enumerations;
using AGEX.CORE.Exceptions;
using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Interfaces.Repositories.Employees;
using AGEX.CORE.Models.Employees;
using System.Data;

namespace AGEX.INFRAESTRUCTURE.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IBaseAgexRepository _baseRepository;
        private readonly string _sp = "sp_employee";

        public EmployeeRepository(IBaseAgexRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> RegisterEmployee(RegisterEmployeeReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "REGISTER_EMPLOYEE" },
                {"@i_employee_name", request.Name},
                {"@i_employee_last_name", request.LastName},
                {"@i_age", Convert.ToInt32(request.Age) },
                {"@i_birthday", request.DateBirthday},
                {"@i_gender", request.Gender },
                {"@i_dpi", request.DPI },
                {"@i_nit", request.NIT},
                {"@i_igss", request.IGSS},
                {"@i_irtra", request.IRTRA},
                {"@i_passport", request.Passport },
                {"@i_workstation_id", Convert.ToInt32(request.WorkstationId)},
                {"@i_address_line_1", request.Line1 },
                {"@i_address_line_2", request.Line2 },
                {"@i_email", request.Email },
                {"@i_phone", request.Phone }
            });

            return ResponseCode.Success;
        }

        public async Task<int> UpdateEmployee(UpdateEmployeeReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "UPDATE_EMPLOYEE" },
                {"@i_employee_name", request.Name},
                {"@i_employee_last_name", request.LastName },
                {"@i_age", Convert.ToInt32(request.Age) },
                {"@i_dpi", request.Dpi},
                {"@i_address_line_1", request.Line1 },
                {"@i_address_line_2", request.Line2 },
                {"@i_email", request.Email },
                {"@i_phone", request.Phone }
            });

            return ResponseCode.Success;
        }

        public async Task<int> DeleteEmployee(DeleteEmployeeReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "DELETE_EMPLOYEE" },
                {"@i_employee_id", Convert.ToInt32(request.Id)},
                {"@i_dpi", request.Dpi}
            });

            return ResponseCode.Success;
        }

        public async Task<List<GetEmployeeResDto>> GetEmployees(GetEmployeeReqDto request)
        {
            List<GetEmployeeResDto> orderModels = new();

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_EMPLOYEES" }
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetEmployees)} (c).");

            foreach (DataRow dr in dt.Rows)
            {
                orderModels.Add(new GetEmployeeResDto
                {
                    Id = dr["EMPLOYEE_ID"].ToString(),
                    Name = dr["EMPLOYEE_NAME"].ToString(),
                    DPI = dr["EMPLOYEE_DPI"].ToString(),
                    Line1 = dr["ADDRESS_LINE_1"].ToString(),
                    Email = dr["EMAIL"].ToString(),
                    message = "Success"
                });
            }

            return orderModels;
        }

        public async Task<List<GetEmployeeResDto>> GetEmployee(GetEmployeeReqDto request)
        {
            List<GetEmployeeResDto> orderModels = new();

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_EMPLOYEE" },
                {"@i_dpi", request.DPI}
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetEmployee)} (c).");

            foreach (DataRow dr in dt.Rows)
            {
                orderModels.Add(new GetEmployeeResDto
                {
                    Id = dr["EMPLOYEE_ID"].ToString(),
                    Name = dr["EMPLOYEE_NAME"].ToString(),
                    DPI = dr["EMPLOYEE_DPI"].ToString(),
                    Line1 = dr["ADDRESS_LINE_1"].ToString(),
                    Email = dr["EMAIL"].ToString(),
                    message = "Success"
                });
            }

            return orderModels;
        }
    }
}

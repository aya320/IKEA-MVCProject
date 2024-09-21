using IKEA.BLL.Models.Department;
using IKEA.BLL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<GetAllEmployeeDto> GetAllEmployees();
        GetEmployeeDetailsDto GetEmployeeById(int id);
        int CreateEmployee(CreateEmployeeDto entity);
        int UpdateEmployee(UpdateEmployeeDto entity);
        bool DeleteEmployee(int id);
    }
}

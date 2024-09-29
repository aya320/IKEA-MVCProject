using IKEA.BLL.Models.Employee;

namespace IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task< IEnumerable<GetAllEmployeeDto>> GetEmployeesAsync(string Search);
        Task< GetEmployeeDetailsDto?> GetEmployeeByIdAsync(int id);
        Task< int> CreateEmployeeAsync(CreateEmployeeDto entity);
        Task< int> UpdateEmployeeAsync(UpdateEmployeeDto entity);
        Task< bool> DeleteEmployeeAsync(int id);
    }
}

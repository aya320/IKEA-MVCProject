using IKEA.BLL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public interface IDepartmentService
    {
        
        Task< IEnumerable<GetAllDepartmentDto>> GetAllDepartmentsAsync();
        Task<  GetDepartmentDetailsDto?> GetDepartmentByIdAsync(int Id);
        Task<  int> CreateDepartmentAsync(CreateDepartmentDto department);
        Task< int> UpdateDepartmentAsync(UpdateDepartmentDto department);
        Task< bool> DeleteDepartmentAsync(int Id);
    }
}

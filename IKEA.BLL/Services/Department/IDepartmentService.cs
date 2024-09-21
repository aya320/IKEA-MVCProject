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
        IEnumerable<GetAllDepartmentDto> GetAllDepartments();
        GetDepartmentDetailsDto? GetDepartmentById(int Id);
        int CreateDepartment(CreateDepartmentDto department);
        int UpdateDepartment(UpdateDepartmentDto department);
        bool DeleteDepartment(int Id);
    }
}

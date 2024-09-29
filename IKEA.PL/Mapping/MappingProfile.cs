using AutoMapper;
using IKEA.BLL.Models.Department;
using IKEA.BLL.Models.Employee;
using IKEA.PL.ViewModels.Departments;
using IKEA.PL.ViewModels.Employees;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region  Department
            CreateMap<DepartmentEditViewModel, CreateDepartmentDto>()/*.ReverseMap()*/;
            CreateMap<GetDepartmentDetailsDto, DepartmentEditViewModel>();
            CreateMap<DepartmentEditViewModel, UpdateDepartmentDto>();
            #endregion

            #region  Department
            CreateMap<EmployeeEditViewModel, CreateEmployeeDto>();
            CreateMap<GetEmployeeDetailsDto, EmployeeEditViewModel>().ForMember(dest => dest.Image, opt => opt.Ignore()); ;
            CreateMap<EmployeeEditViewModel, UpdateEmployeeDto>();
            #endregion

        }
    }
}

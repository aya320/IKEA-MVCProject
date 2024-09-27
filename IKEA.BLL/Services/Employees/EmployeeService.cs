using IKEA.BLL.Models.Employee;
using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository  _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository =  employeeRepository;
        }
        public int CreateEmployee(CreateEmployeeDto entity)
        {
            var employee = new Employee()
            {
               
                Name = entity.Name,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                HiringDate = entity.HiringDate,
                Address = entity.Address,
                Email = entity.Email,
                Age = entity.Age,
                LastModifiedBy = 1,
                CreatedBy=1,
                LastModifiedOn = DateTime.UtcNow,
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                DepartmentId = entity.DepartmentId,
                //EmployeeType = entity.EmployeeType,

                

            };
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
          var employee =_employeeRepository.GetById(id);
            if(employee is { })
               return _employeeRepository.Delete(employee)>0;
            else
                return false;

        }

        public IEnumerable<GetAllEmployeeDto> GetEmployees(string Search)
        {
            var employees = _employeeRepository.GetIQueryable().Where(a => !a.IsDeleted && (string.IsNullOrEmpty(Search) || a.Name.ToUpper().Contains(Search.ToUpper()))).Select(entity => new GetAllEmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                Address = entity.Address,
                Email = entity.Email,
                Age = entity.Age,
                Gender = entity.Gender,
                //EmployeeType = entity.EmployeeType,
            }).ToList();
           

            //var firstEmployee = query.FirstOrDefault();

            //var count = query.Count();
            return employees;
        }

        public GetEmployeeDetailsDto GetEmployeeById(int id)
        {
            var entity = _employeeRepository.GetById(id);

            if (entity is { })
                return new GetEmployeeDetailsDto()
                {


                    Name = entity.Name,
                    Salary = entity.Salary,
                    IsActive = entity.IsActive,
                    HiringDate = entity.HiringDate,
                    Address = entity.Address,
                    Email = entity.Email,
                    Age = entity.Age,
                    PhoneNumber = entity.PhoneNumber,
                    Gender = entity.Gender,
                    LastModifiedBy = entity.LastModifiedBy,
                    CreatedBy = entity.CreatedBy,
                    CreatedOn = entity.CreatedOn,
                    LastModifiedOn = entity.LastModifiedOn,
                    //EmployeeType = entity.EmployeeType,
                };
            else
                return null;
        }

        public int UpdateEmployee(UpdateEmployeeDto entity)
        {
            var employee = new Employee()
            {
                Id = entity.Id,
                Name = entity.Name,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                HiringDate = entity.HiringDate,
                Address = entity.Address,
                Email = entity.Email,
                Age = entity.Age,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                DepartmentId = entity.DepartmentId,
                //EmployeeType = entity.EmployeeType,

            };
            return _employeeRepository.Update(employee);
        }
    }
}

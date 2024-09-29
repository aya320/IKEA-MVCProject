using IKEA.BLL.Models.Common.Services.Attachment;
using IKEA.BLL.Models.Employee;
using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;


        public EmployeeService(IAttachmentService attachmentService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
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

            if (entity is not null)
                employee.Image = _attachmentService.Upload(entity.Image, "Images");
            _unitOfWork.EmployeeRepository.Add(employee);
              return _unitOfWork.Compelete();
        }

        public bool DeleteEmployee(int id)
        {
            var emp = _unitOfWork.EmployeeRepository;
          var employee = emp.GetById(id);
            if(employee is { })
                emp.Delete(employee);
           
                return _unitOfWork.Compelete()>0;

        }

        public IEnumerable<GetAllEmployeeDto> GetEmployees(string Search)
        {
            var employees = _unitOfWork.EmployeeRepository.GetIQueryable().Where(a => !a.IsDeleted && (string.IsNullOrEmpty(Search) || a.Name.ToUpper().Contains(Search.ToUpper()))).Include(x=>x.Department).Select(entity => new GetAllEmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                Address = entity.Address,
                Email = entity.Email,
                Age = entity.Age,
                Gender = entity.Gender,
                DepartmentId=entity.Department.Id,
                Department = entity.Department.Name ,
                
                //EmployeeType = entity.EmployeeType,
            }).ToList();
           

            //var firstEmployee = query.FirstOrDefault();

            //var count = query.Count();
            return employees;
        }

        public GetEmployeeDetailsDto GetEmployeeById(int id)
        {
            var entity = _unitOfWork.EmployeeRepository.GetById(id);

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
                    DepartmentId = entity.Department?.Id,
                    Department = entity.Department?.Name,

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
             _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.Compelete();
        }
    }
}

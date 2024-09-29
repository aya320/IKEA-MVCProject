using IKEA.BLL.Models.Common.Services.Attachment;
using IKEA.BLL.Models.Employee;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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
        public async Task< int> CreateEmployeeAsync(CreateEmployeeDto entity)
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
            {
                employee.Image =await _attachmentService.UploadAsync(entity.Image, "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee);
              return await _unitOfWork.CompeleteAsync();
        }

        public async Task< bool> DeleteEmployeeAsync(int id)
        {
            var emp = _unitOfWork.EmployeeRepository;
          var employee =await emp.GetByIdAsync(id);
            if(employee is { })
                emp.Delete(employee);
           
                return await _unitOfWork.CompeleteAsync()>0;

        }

        public async Task< IEnumerable<GetAllEmployeeDto>> GetEmployeesAsync(string Search)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetIQueryable().Where(a => !a.IsDeleted && (string.IsNullOrEmpty(Search) || a.Name.ToUpper().Contains(Search.ToUpper()))).Include(x=>x.Department).Select(entity => new GetAllEmployeeDto
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
            }).ToListAsync();
           

            //var firstEmployee = query.FirstOrDefault();

            //var count = query.Count();
            return employees;
        }

        public async Task< GetEmployeeDetailsDto> GetEmployeeByIdAsync(int id)
        {
            var entity =await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (entity is { })
                return  new GetEmployeeDetailsDto()
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
                    Image= entity.Image,

                    //EmployeeType = entity.EmployeeType,
                };
            else
                return null;
        }

        public async Task< int> UpdateEmployeeAsync(UpdateEmployeeDto entity)
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

            if (entity.Image is not null)
                employee.Image = await _attachmentService.UploadAsync(entity.Image, "Images");

            _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.CompeleteAsync();
        }
    }
}

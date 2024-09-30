using IKEA.BLL.Models.Department;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task< int> CreateDepartmentAsync(CreateDepartmentDto department)
        {
            var CreatedDepartment = new IKEA.DAL.Entities.Departments.Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = 1,
                //CreatedOn = department.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
             _unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return await _unitOfWork.CompeleteAsync();
        }

        public async Task< bool> DeleteDepartmentAsync(int Id)
        {
            var dept =  _unitOfWork.DepartmentRepository;
           var department = await dept.GetByIdAsync(Id);
            if (department is { })
            
                 dept.Delete(department);
          
                return await _unitOfWork.CompeleteAsync()>0;
        }

        public async Task< IEnumerable<GetAllDepartmentDto>> GetAllDepartmentsAsync()
        {
          var deparments = await _unitOfWork.DepartmentRepository.GetIQueryable().Where(a => !a.IsDeleted).Select(department => new GetAllDepartmentDto
          {
              Id = department.Id,
              Name = department.Name,
             
              CreationDate = department.CreationDate,
              Code = department.Code,
          }
              
              ).AsNoTracking().ToListAsync();
            return  deparments; 
        }

        public async Task< GetDepartmentDetailsDto?> GetDepartmentByIdAsync(int Id)
        {
           var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(Id);
            if (department is { })
            {
                return  new GetDepartmentDetailsDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    Code = department.Code,
                    LastModifiedBy = department.LastModifiedBy,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedOn = department.LastModifiedOn,
                };
            }
            else
                return null;

           
        }

        public async Task< int> UpdateDepartmentAsync(UpdateDepartmentDto department)
        {
            var UpdatedDepartment = new IKEA.DAL.Entities.Departments.Department()
            {
                Id= department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
             _unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return await _unitOfWork.CompeleteAsync();
        }
    }
}

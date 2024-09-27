using IKEA.BLL.Models.Department;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CreateDepartment(CreateDepartmentDto department)
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
            return _unitOfWork.Compelete();
        }

        public bool DeleteDepartment(int Id)
        {
            var dept = _unitOfWork.DepartmentRepository;
           var department = dept.GetById(Id);
            if (department is { })
            
                 dept.Delete(department);
          
                return _unitOfWork.Compelete()>0;
        }

        public IEnumerable<GetAllDepartmentDto> GetAllDepartments()
        {
          var deparments = _unitOfWork.DepartmentRepository.GetIQueryable().Where(a => !a.IsDeleted).Select(department => new GetAllDepartmentDto
          {
              Id = department.Id,
              Name = department.Name,
             
              CreationDate = department.CreationDate,
              Code = department.Code,
          }
              
              ).AsNoTracking().ToList();
            return deparments; 
        }

        public GetDepartmentDetailsDto? GetDepartmentById(int Id)
        {
           var department = _unitOfWork.DepartmentRepository.GetById(Id);
            if (department is { })
            {
                return new GetDepartmentDetailsDto
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

        public int UpdateDepartment(UpdateDepartmentDto department)
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
            return _unitOfWork.Compelete();
        }
    }
}

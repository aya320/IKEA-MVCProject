using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public int Add(Department entity)
        {
            _dbcontext.Departments.Add(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbcontext.Departments.Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            { 
              return _dbcontext.Departments.AsNoTracking().ToList();
            }
            else
                return _dbcontext.Departments.ToList();


        }

        public IQueryable<Department> GetAllAsQueryable()
        {
              return _dbcontext.Departments;

        }

        public Department GetById(int id)
        {
            var department = _dbcontext.Departments.Find(id);
            return department;
        }
        

        public int Update(Department entity)
        {
            _dbcontext.Departments.Update(entity);
            return _dbcontext.SaveChanges();
        }
    }
}

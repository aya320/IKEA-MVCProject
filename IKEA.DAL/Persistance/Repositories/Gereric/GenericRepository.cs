using IKEA.DAL.Entities;
using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Gereric
{
    public class GenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbcontext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public int Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                return _dbcontext.Set<T>().AsNoTracking().ToList();
            }
            else
                return _dbcontext.Set<T>().ToList();


        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbcontext.Set<T>();

        }

        public T GetById(int id)
        {
            var Result = _dbcontext.Set<T>().Find(id);
            return Result;
        }


        public int Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            return _dbcontext.SaveChanges();
        }
    }
}

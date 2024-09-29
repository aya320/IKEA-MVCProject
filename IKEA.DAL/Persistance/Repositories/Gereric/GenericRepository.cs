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
    public class GenericRepository<T>:IGenericRepository<T> where T : ModelBase 
    {
        private protected readonly ApplicationDbContext _dbcontext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public void Add(T entity)=>  _dbcontext.Set<T>().Add(entity);
            

        public void Delete(T entity)
        {
            entity.IsDeleted=true;
            _dbcontext.Set<T>().Update(entity);
         
        }

        public async Task< IEnumerable<T>> GetAllAsync (bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                return await _dbcontext.Set<T>().Where(a=>!a.IsDeleted).AsNoTracking().ToListAsync();
            }
            else
                return await _dbcontext.Set<T>().Where(a => !a.IsDeleted).ToListAsync();


        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbcontext.Set<T>();

        }
        public IEnumerable<T> GetIEnumerable()
        {
            throw new NotImplementedException();
        }

        public async Task< T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }


        public void Update(T entity)=>  _dbcontext.Set<T>().Update(entity);
         
       

      
    }
}

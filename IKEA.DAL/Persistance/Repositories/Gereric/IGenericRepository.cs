using IKEA.DAL.Entities;
using IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Gereric
{
    public interface IGenericRepository<T> where T : ModelBase
    {
       Task< IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);

        IQueryable<T> GetIQueryable();
        IEnumerable<T> GetIEnumerable();


        Task< T?> GetByIdAsync(int id);
       void Add(T entity);
       void Update(T entity);
       void Delete(T entity);
    }
}

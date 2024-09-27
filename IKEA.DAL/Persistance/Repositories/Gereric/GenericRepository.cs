﻿using IKEA.DAL.Entities;
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

        public IEnumerable<T> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                return _dbcontext.Set<T>().Where(a=>!a.IsDeleted).AsNoTracking().ToList();
            }
            else
                return _dbcontext.Set<T>().Where(a => !a.IsDeleted).ToList();


        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbcontext.Set<T>();

        }
        public IEnumerable<T> GetIEnumerable()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }


        public void Update(T entity)=>  _dbcontext.Set<T>().Update(entity);
         
       

      
    }
}

﻿using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Gereric;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
    }
}

using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Gereric;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

    }
}

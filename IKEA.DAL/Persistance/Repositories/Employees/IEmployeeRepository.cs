using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Persistance.Repositories.Gereric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
       

    }
}

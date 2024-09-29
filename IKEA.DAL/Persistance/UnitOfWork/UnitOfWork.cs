using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
             _context = context;
        }
        public IDepartmentRepository DepartmentRepository =>new DepartmentRepository(_context);

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_context);

        public async Task<int> CompeleteAsync()
        {
           return await _context.SaveChangesAsync();

        }
        public async ValueTask DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}

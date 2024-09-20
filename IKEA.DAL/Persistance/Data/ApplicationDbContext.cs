using IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplicationDbContext :DbContext 
    {
        public ApplicationDbContext():base()
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()) ;
               
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= . ;Database = IKEA; Trusted_Connection = True /*MultipleActiveResultSets=True*/ ");
        }

        public DbSet<Department> Departments { get; set; }
    }
}

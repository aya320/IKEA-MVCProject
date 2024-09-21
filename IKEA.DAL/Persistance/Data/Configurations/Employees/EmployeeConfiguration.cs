using IKEA.DAL.Common.Enums;
using IKEA.DAL.Entities.Departments;
using IKEA.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.Employees
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8, 2)");
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql().HasComputedColumnSql("GETDATE()");

            builder.Property(E => E.Gender).HasConversion(

            (gender) => gender.ToString(),
            (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
            
            );

            builder.Property(E => E.EmployeeType).HasConversion(

           (employeeType) => employeeType.ToString(),
           (employeeType) => (EmployeeType)Enum.Parse(typeof(Gender), employeeType)

           );
        }
    }
}

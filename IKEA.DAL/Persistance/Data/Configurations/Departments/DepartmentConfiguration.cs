using IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
             builder.Property(A=>A.Id).UseIdentityColumn(10,10);
            builder.Property(A => A.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(A => A.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(A => A.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(A => A.LastModifiedOn).HasComputedColumnSql().HasComputedColumnSql("GETDATE()");

                
                ;

        }
    }
}

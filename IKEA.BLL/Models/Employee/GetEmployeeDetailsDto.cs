using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Employee
{
    public class GetEmployeeDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string Address { get; set; } = null!;
        public DateTime HiringDate { get; set; }
        public string? PhoneNumber { get; set; }

        public decimal Salary { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        //public EmployeeType EmployeeType { get; set; }
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int? DepartmentId { get; set; }

    }
}

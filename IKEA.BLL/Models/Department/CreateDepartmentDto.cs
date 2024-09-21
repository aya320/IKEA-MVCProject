using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Department
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string Code { get; set; } = null!;

        [Display(Name = "Date Of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}

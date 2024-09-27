using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    { 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string Code { get; set; } = null!;

        [Display(Name =("Date Of Creation"))]
        public DateOnly CreationDate { get; set; }
    }
}

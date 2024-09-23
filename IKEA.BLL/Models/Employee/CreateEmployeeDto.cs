﻿using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Employee
{
    public class CreateEmployeeDto
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Letters ")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Letters ")]
       public string Name { get; set; } = null!;

        [Range(22, 30)]
       public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; } 

        //[Display(DataType.Currency)]
        public decimal Salary { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name ="Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        //public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Is Active")]

        public bool IsActive { get; set; }
        public int? DepartmentId { get; set; }
    }
}

﻿using IKEA.BLL.Models.Department;
using IKEA.BLL.Services.Department;
using IKEA.DAL.Entities.Departments;
using IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [HttpGet]
        public IActionResult Index()
        {  //
           // ViewData["Message"] = "Hello Yoyo";
           // ViewBag.Message = "Hello Ayoy";
            var department = _departmentService.GetAllDepartments();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]


        public IActionResult Create(DepartmentEditViewModel departmentvm)
        {
            if (!ModelState.IsValid)
                return View(departmentvm);

            var Message = string.Empty;
            try
            {
                var Created = new CreateDepartmentDto()
                {
                    Code = departmentvm.Code,
                    Name = departmentvm.Name,
                    CreationDate = departmentvm.CreationDate,
                    Description = departmentvm.Description,
                };
                var department = _departmentService.CreateDepartment(Created);
                if (department > 0)
                    TempData["Message"] = "Created Successfully";  
                else
                    TempData["Message"] = "Failed To Create ";


                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Create";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentvm);
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var deppartment = _departmentService.GetDepartmentById(Id.Value);

            if (deppartment == null)
                return NotFound();

            return View(deppartment);
        }

        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id == null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(Id.Value);
            if (department == null)
                return NotFound();
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,

            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Update([FromRoute] int id, DepartmentEditViewModel departmentvm)
        {
            if (!ModelState.IsValid)
                return View(departmentvm);
            var Message = string.Empty;
            try
            {
                var department = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = departmentvm.Code,
                    Name = departmentvm.Name,
                    Description = departmentvm.Description,
                    CreationDate = departmentvm.CreationDate,
                };
                var departmentUpdate = _departmentService.UpdateDepartment(department) > 0;
                if (departmentUpdate)

                    TempData["Message"] = "Updated Successfully";


                else

                    TempData["Message"] = "Failed To Update ";


                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Update";


            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentvm);
        }


        //[HttpGet]
        //public IActionResult Delete(int? Id) 
        //{
        //  return View();
        //}

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Message = string.Empty;
            try
            {
                var Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction("Index");
                else
                    Message = "Failed To Delete";


            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Delete";


            }

            return RedirectToAction(nameof(Index));
        }

    }
}

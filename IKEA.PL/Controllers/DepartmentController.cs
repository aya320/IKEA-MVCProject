using IKEA.BLL.Models.Department;
using IKEA.BLL.Services.Department;
using Microsoft.AspNetCore.Mvc;

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
        }

        [HttpGet]
        public IActionResult Index()
        {
            var department =_departmentService.GetAllDepartments();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
           if(!ModelState.IsValid)
                return View(departmentDto);

           var Message = string.Empty;
            try
            {
                var department= _departmentService.CreateDepartment(departmentDto);
                if (department > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError(string.Empty, "Failed To Create");
                return View(departmentDto);

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    Message = ex.Message;
                    return View(departmentDto);
                }
                else
                {
                    Message = "Department is not created";
                    return View("Error", Message);

                }

            }
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var deppartment = _departmentService.GetDepartmentById(Id.Value);

            if(deppartment == null) 
                return NotFound();

            return View(deppartment);
        }

    }
}

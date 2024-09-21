using IKEA.BLL.Models.Department;
using IKEA.BLL.Models.Employee;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Entities.Employees;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public IActionResult Index()
        {
            var employees=_employeeService.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateEmployeeDto  employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            var Message = string.Empty;
            try
            {
                var employee = _employeeService.CreateEmployee(employeeDto);
                if (employee > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError(string.Empty, "Failed To Add");
                return View(employeeDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Add";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employeeDto);
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(Id.Value);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

    }
}

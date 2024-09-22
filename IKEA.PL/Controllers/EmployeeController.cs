using IKEA.BLL.Models.Department;
using IKEA.BLL.Models.Employee;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Entities.Employees;
using IKEA.PL.ViewModels.Departments;
using IKEA.PL.ViewModels.Employees;
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
        //[ValidateAntiForgeryToken]

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

        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id == null)
                return BadRequest();
            var entity = _employeeService.GetEmployeeById(Id.Value);
            if (entity == null)
                return NotFound();
            return View(new EmployeeEditViewModel()
            {

                Name = entity.Name,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                HiringDate = entity.HiringDate,
                Address = entity.Address,
                Email = entity.Email,
                Age = entity.Age,
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
            

            });
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Update([FromRoute] int id, EmployeeEditViewModel employeevm)
        {
            if (!ModelState.IsValid)
                return View(employeevm);
            var Message = string.Empty;
            try
            {
                var entity = new UpdateEmployeeDto()
                {
                    Id = id,
                    Name = employeevm.Name,
                    Salary = employeevm.Salary,
                    IsActive = employeevm.IsActive,
                    Address = employeevm.Address,
                    Age= employeevm.Age,
                    PhoneNumber = employeevm.PhoneNumber,
                    Gender = employeevm.Gender,
                    Email = employeevm.Email,
                    HiringDate= employeevm.HiringDate,
                   
                };
                var EmployeeUpdate = _employeeService.UpdateEmployee(entity) > 0;
                if (EmployeeUpdate)
                    return RedirectToAction("Index");
                else
                    Message = "Failed To Update";


            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Update";


            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employeevm);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            var Message = string.Empty;
            try
            {
                var Deleted = _employeeService.DeleteEmployee(id);
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

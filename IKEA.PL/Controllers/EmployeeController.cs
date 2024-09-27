using AutoMapper;
using IKEA.BLL.Models.Department;
using IKEA.BLL.Models.Employee;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Entities.Departments;
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
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;



        public EmployeeController(IDepartmentService departmentService, IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment, IMapper mapper )
        {
            _employeeService = employeeService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _departmentService = departmentService;
            _mapper = mapper;
        }
        public IActionResult Index(string Search)
        {
            var employees=_employeeService.GetEmployees(Search);
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] =_departmentService.GetAllDepartments();
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeEditViewModel  employeeVm)
        {
            if (!ModelState.IsValid)
                return View(employeeVm);

            var Message = string.Empty;
            try
            {
                //var Employee = new CreateEmployeeDto()
                //{
                //         Address = employeeVm.Address,
                //         Salary = employeeVm.Salary,
                //         Age = employeeVm.Age,
                //         Name = employeeVm.Name,
                //         DepartmentId = employeeVm.DepartmentId,
                //         Email = employeeVm.Email,
                //         Gender = employeeVm.Gender,
                //         PhoneNumber = employeeVm.PhoneNumber,
                //         HiringDate = employeeVm.HiringDate,
                //         IsActive = employeeVm.IsActive,
                         

                //};

                var Employee =_mapper.Map<CreateEmployeeDto>(employeeVm);
                var employee = _employeeService.CreateEmployee(Employee);
                if (employee > 0)
                    TempData["Message"] = "Created Successfully";
                else
                    TempData["Message"] = "Failed To Create ";


                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Message = _environment.IsDevelopment() ? ex.Message : "Failed To Add";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employeeVm);
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
            ViewData["Departments"] = _departmentService.GetAllDepartments();

            var Employee =_mapper.Map<GetEmployeeDetailsDto,EmployeeEditViewModel>(entity);
            return View(Employee);
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
                //var entity = new UpdateEmployeeDto()
                //{
                //    Id = id,
                //    Name = employeevm.Name,
                //    Salary = employeevm.Salary,
                //    IsActive = employeevm.IsActive,
                //    Address = employeevm.Address,
                //    Age= employeevm.Age,
                //    PhoneNumber = employeevm.PhoneNumber,
                //    Gender = employeevm.Gender,
                //    Email = employeevm.Email,
                //    HiringDate= employeevm.HiringDate,
                   
                //};
                var entity=_mapper.Map<UpdateEmployeeDto>(employeevm);
                var EmployeeUpdate = _employeeService.UpdateEmployee(entity) > 0;
                if (EmployeeUpdate)
                
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

using AutoMapper;
using IKEA.BLL.Models.Department;
using IKEA.BLL.Services.Department;
using IKEA.DAL.Entities.Departments;
using IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace IKEA.PL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment, IMapper mapper )
        {
            _departmentService = departmentService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
           _mapper = mapper;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {  //
           // ViewData["Message"] = "Hello Yoyo";
           // ViewBag.Message = "Hello Ayoy";
            var department =await _departmentService.GetAllDepartmentsAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]


        public async Task< IActionResult> Create(DepartmentEditViewModel departmentvm)
        {
            if (!ModelState.IsValid)
                return View(departmentvm);

            var Message = string.Empty;
            try
            {
                //var Created = new CreateDepartmentDto()
                //{
                //    Code = departmentvm.Code,
                //    Name = departmentvm.Name,
                //    CreationDate = departmentvm.CreationDate,
                //    Description = departmentvm.Description,
                //};
                var Created = _mapper.Map<CreateDepartmentDto>(departmentvm);
                var department = await _departmentService.CreateDepartmentAsync(Created);
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
        public async Task< IActionResult> Details(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var deppartment =await _departmentService.GetDepartmentByIdAsync(Id.Value);

            if (deppartment == null)
                return NotFound();

            return View(deppartment);
        }

        [HttpGet]
        public async Task< IActionResult> Update(int? Id)
        {
            if (Id == null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(Id.Value);
            if (department == null)
                return NotFound();

            var departmentvm=_mapper.Map<GetDepartmentDetailsDto,DepartmentEditViewModel>(department);
            return View(departmentvm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task< IActionResult> Update([FromRoute] int id, DepartmentEditViewModel departmentvm)
        {
            if (!ModelState.IsValid)
                return View(departmentvm);
            var Message = string.Empty;
            try
            {
                //var department = new UpdateDepartmentDto()
                //{
                //    Id = id,
                //    Code = departmentvm.Code,
                //    Name = departmentvm.Name,
                //    Description = departmentvm.Description,
                //    CreationDate = departmentvm.CreationDate,
                //};

                var department = _mapper.Map<UpdateDepartmentDto>(departmentvm);

                var departmentUpdate =await _departmentService.UpdateDepartmentAsync(department) > 0;
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
        public async Task< IActionResult >Delete(int id)
        {
            var Message = string.Empty;
            try
            {
                var Deleted = await _departmentService.DeleteDepartmentAsync(id);
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

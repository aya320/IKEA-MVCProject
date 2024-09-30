using IKEA.DAL.Entities.Identity;
using IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await _userManager.FindByNameAsync(model.UserName);
            if (User == null)
            {
                User = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    UserName = model.UserName,

                };
                var result = await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This User Is Already Exist");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
			if (!ModelState.IsValid)
				return BadRequest();
			var User = await _userManager.FindByNameAsync(model.Email);
            if(User is { })
            {
                var Flag = await _userManager.CheckPasswordAsync(User, model.Password);
                if(Flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(User ,model.Password,model.RememberMe,true);

					if (result.IsNotAllowed)

						ModelState.AddModelError(string.Empty, "Your account is not confirmed yet ");

					if (result.IsLockedOut)

						ModelState.AddModelError(string.Empty, "Your account is locked ");

					if (result.Succeeded)

						return RedirectToAction(nameof(HomeController.Index), "Home");
				}

               
            }
			ModelState.AddModelError(nameof(SignUpViewModel.UserName), " Failed To Login ");

			return View(model);

		}
	}
}

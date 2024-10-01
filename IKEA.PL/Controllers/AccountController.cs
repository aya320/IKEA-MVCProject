using IKEA.DAL.Entities.Email;
using IKEA.DAL.Entities.Identity;
using IKEA.PL.Helpers;
using IKEA.PL.Settings;
using IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly IMailSettings _mailSettings;
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(IMailSettings mailSettings,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_mailSettings = mailSettings;
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
			var User = await _userManager.FindByEmailAsync(model.Email);
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
					{
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                        //return Redirect("https://www.example.com");
                        //return RedirectToRoute(new { controller = "HomeController", action = "Index" });

                        //string url = Url.Action("Index", "HomeController");
                        //return Redirect(url);
                        

                    }
				}

               
            }
			ModelState.AddModelError(nameof(SignUpViewModel.UserName), " Failed To Login ");

			return View(model);

		}
        public async new Task <IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        [HttpGet]
        public IActionResult CheckYourEmail()
        {
            return View();
        }

		[HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user is not null)
				{
					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

					var passwordUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token = resetPasswordToken }, Request.Scheme);
		
					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset password",
						Body = passwordUrl
					};
					_mailSettings.SendEmail(email);
					return Redirect(nameof(CheckYourEmail));
				}
				ModelState.AddModelError(string.Empty, "There is not account with this email");
			}
			return View(model);
		}


        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user is { })
                {
                    _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    return RedirectToAction(nameof(SignIn));
                }
                ModelState.AddModelError(string.Empty, "Url is not valid");
            }
            return View(model);
        }




    }
}

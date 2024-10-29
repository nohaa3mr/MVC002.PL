using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC002.DAL.Models;
using MVC002.PL.Helpers;
using MVC002.PL.ViewModels;
using System;
using System.Threading.Tasks;

namespace MVC002.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _user;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> user , SignInManager<ApplicationUser> signInManager)
        {
			_user = user;
			_signInManager = signInManager;
		}
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model == null)
                return View("Error");
            var User = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                FName = model.FirstName,
                LName = model.LastName,
                IsAgree  = model.IsAgree,
            };


          var result = await _user.CreateAsync(User, model.Password);
            if (result.Succeeded)

				return RedirectToAction(nameof(Index), "Home");

			else
				foreach (var error in result.Errors )
              
                    ModelState.AddModelError(string.Empty,  error.Description);
             return View(model);
               
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await _user.FindByEmailAsync(login.Email);
            if (user != null)
            {
              var flag = await  _user.CheckPasswordAsync(user, login.Password);
                if (flag)
                {
                   var Result =  await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(Index), "Home");
                }
                else
                { ModelState.AddModelError(string.Empty, "Password is not existed.");
                }
			}
            else
                ModelState.AddModelError(string.Empty, "Email address is not existed.");
            return View(login);
        }


        [HttpGet]
        public new async Task<IActionResult> SignOut() //to hide the actual intend of signout  
        {

           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();

        }

        [HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{

            var User = await _user.FindByEmailAsync(model.Email);
            if(User != null)
            {
                var token = await _user.GeneratePasswordResetTokenAsync(User);
                var ResetPasswordLink = Url.Action("ResetPassword", "Account",new {email = User.Email , Token = token},Request.Scheme );
                var email = new EmailAddress()
                {
                    To = model.Email,
                    Subject = $"Reset Password for {model.Email}",
                    Body = ResetPasswordLink,
                };
                EmailSettings.SendByEmail(email);
                return RedirectToAction("CheckYourMail");
            }
			return View("ForgetPassword " , model);

		}

        public IActionResult CheckYourMail()
        {
            return View();

        }

        [HttpGet]
        public IActionResult ResetPassword( string email , string token)
        {
            TempData["email"] = email;
			TempData["token"] = token;


			return View();
        }


        [HttpPost]
		public async Task <IActionResult>ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
				string token = TempData["token"] as string;

               var user=await _user.FindByEmailAsync(email);
              var Result= await  _user.ResetPasswordAsync(user, token, model.NewPassword);
                if (Result.Succeeded)
                
                    return RedirectToAction("Login");
                
                else 
                    foreach (var error in Result.Errors)
                    
                        ModelState.AddModelError(string.Empty, error.Description);
                    
                
			}

			return View(model);
		}
	}
}

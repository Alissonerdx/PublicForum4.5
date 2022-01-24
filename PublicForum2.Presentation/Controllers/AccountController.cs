using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PublicForum2.Infra.Identity.Configuration;
using PublicForum2.Infra.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PublicForum2.Presentation.Helpers;
using System.Security.Claims;

namespace PublicForum2.Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            _signInManager.AuthenticationManager.SignOut();
            return Redirect("/Account/Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Fill in all mandatory fields" });

            var user = new ApplicationUser
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
                return JsonContent(new { Success = true, Message = "User registered succefully" });

            return JsonContent(new { Success = false, Message = String.Join("<br/>", result.Errors) });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return JsonContent(new { Success = false, Message = "Fill in all mandatory fields" });

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return JsonContent(new { Success = false, Message = "Username or Password is invalid" });

            var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, shouldLockout: true);

            if (result != SignInStatus.Success)
            {
                return JsonContent(new { Success = false, Message = "Username or Password is invalid" });
            }

            return JsonContent(new { Success = true, Message = "User logged", Redirect = "/Topic/Index" });
        }
    }
}
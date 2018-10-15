using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ERPManager.Entities.Model.Identity;
using ERPManager.Web.Models.Account;
using ERPManager.Service.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ERPManager.Web.Controllers
{
    public class AccountController : Controller
    {
        UserManager<CustomIdentityUser> _userManager;
        RoleManager<CustomIdentityRole> _roleManager;
        SignInManager<CustomIdentityUser> _signInManager;
        IHttpContextAccessor _httpContextAccessor;
        ILanguageService _languageService;

        public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager, ILanguageService languageService
            ,IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _languageService = languageService;
            _httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            model.AvailableLanguages = _languageService.GetAll().Select(k=> new SelectListItem { Text = k.Name, Value = k.Id }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult  Login(LoginModel model)
        {

            //var user = new CustomIdentityUser { UserName = model.UserName, Email = model.UserName };
            //var signinresult = _userManager.CreateAsync(user, model.Password);

            if (ModelState.IsValid)
            {

                var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;
                var roles = _roleManager.Roles.ToList();
                if (result.Succeeded)
                {
                   _httpContextAccessor.HttpContext.Response.Cookies.Append("culture", model.LanguageId, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.UtcNow.AddYears(1), Path = "/", HttpOnly = true });
                    return Redirect("/Home/Index");
                }
                TempData["message"] = "Invalid Login";
            }
            

            model.AvailableLanguages = _languageService.GetAll().Select(k => new SelectListItem { Text = k.Name, Value = k.Id }).ToList();

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.NewPassword != model.NewPasswordAgain)
                {
                    model.Errors.Add( "Passwords Doesn't Match");
                    return View(model);
                }

                var user=_userManager.GetUserAsync(this.User).Result;
                var result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;

                if(result.Succeeded)
                {
                    model.SuccessMessage = "Password is changed";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Errors = result.Errors.Select(k => k.Description).ToList();
                }
            }

              
            return View(model);
        }

        public ActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();
            TempData["message"] = "Successfully Logged Out";
            return RedirectToAction("Login");

        }

        public ActionResult AccessDenied()
        {
           
            return View();

        }

        
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcFreelan.Data;
using MvcFreelan.Models;
using MvcFreelan.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcFreelan.Controllers
{
    
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
                                UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //   var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");

                    ModelState.AddModelError(string.Empty, "Invalid Log Attempt");
                }
                return View(model);
            }
            catch(Exception ex)
            {
                var err = new ErrorViewModel();
                err.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", err); // Or a specific error view
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Name, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AdminSet()
        {
            string conn = _configuration.GetConnectionString("conn");
            var dbRenta = new DbRenta(conn);
            var resp = dbRenta.AdminSet();

            if (resp.Contains("OK"))
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("AccessDenied");

        }
    }
}

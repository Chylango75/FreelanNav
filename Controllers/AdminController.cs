using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MvcFreelan.ViewModels;
using System.Data;

namespace MvcFreelan.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        ///////////////////////////////////////   Register    /////////////////////////////////

        public IActionResult Users()
        {
            var  users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = new IdentityUser();
            user.UserName = "No User";
            user = await _userManager.FindByIdAsync(id);

            var allRoles = await _roleManager.Roles.ToListAsync();
            var currRoles = await _userManager.GetRolesAsync(user);
            var userRoles = new List<RoleViewModel>();

            foreach (var r in allRoles)
            {
                var roleVM = new RoleViewModel();
                roleVM.Id = r.Id;
                roleVM.Name = r.Name;
                roleVM.IsSelected = currRoles.Contains(r.Name);
                userRoles.Add(roleVM);
            }
            /*
            var newRol = new IdentityRole("Admin");
            var result = await _userManager.AddToRoleAsync(user, newRol.Name);
            */
            var userVM = new UserViewModel()
            {
                User = user,
                Roles = userRoles
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.User.Id);

                if (user == null)
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

                // Remove existing roles
                //var newRoles = await _userManager.GetRolesAsync(user);

                var currentRoles = await _userManager.GetRolesAsync(user);
                var resultRolesRemove = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                currentRoles = await _userManager.GetRolesAsync(user);

                if (resultRolesRemove.Succeeded)
                {
                    //  Add each new Role
                    foreach (var role in model.Roles)
                    {
                        if (role.IsSelected)
                        {
                            var newRol = new IdentityRole(role.Name);
                            var resultAddRole = await _userManager.AddToRoleAsync(user, newRol.Name);
                        }
                    }

                    user.Email = model.User.Email;
                    user.PhoneNumber = model.User.PhoneNumber;
                    user.UserName = model.User.UserName;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction("Users");

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    // Handle errors (e.g., result.Errors)
                }
            }

            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var allRoles = await _roleManager.Roles.ToListAsync();
            return View(allRoles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));

            return RedirectToAction("Roles");
        }


        //////////////////////////////////////////////////////////////////////////////////

        [AllowAnonymous]
        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var path = "/pics/000Error.jpg";

            if (statusCode > 0)
            {
                if (statusCode >= 400 && statusCode <= 500)
                    //path = "";
                    path = "/pics/400NotFound.jpg";
                if (statusCode >= 500)
                    //path = "";
                    path = "/Pics/500InternalError.jpg";
            }

            ViewBag.path = path;

            return View();
        }
    }
}

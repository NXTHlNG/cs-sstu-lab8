using cs_sstu_lab8.Data;
using cs_sstu_lab8.Models;
using cs_sstu_lab8.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cs_sstu_lab8.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser 
                {
                    FullName = model.FullName,
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            EditUserModel model = new EditUserModel
            {
                Id = user.Id,
                FullName = user.FullName,
                EmailAddress = user.Email
            };

            if (user == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        FullName = model.FullName,
                        Email = model.EmailAddress,
                        UserName = model.EmailAddress
                    };

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditRoles(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();

                ChangeRolesModel model = new ChangeRolesModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(string id, List<string> roles)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: UsersController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}

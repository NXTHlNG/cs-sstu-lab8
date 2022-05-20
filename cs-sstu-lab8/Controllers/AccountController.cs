using cs_sstu_lab8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs_sstu_lab8.ViewModels;
using cs_sstu_lab8.Data;

namespace cs_sstu_lab8.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login() => View(new LoginModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid) return View(loginModel);

            var user = await _userManager.FindByEmailAsync(loginModel.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginModel);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginModel);
        }

        public IActionResult Register() => View(new RegisterModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid) return View(registerModel);

            var user = await _userManager.FindByEmailAsync(registerModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerModel);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerModel.FullName,
                Email = registerModel.EmailAddress,
                UserName = registerModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
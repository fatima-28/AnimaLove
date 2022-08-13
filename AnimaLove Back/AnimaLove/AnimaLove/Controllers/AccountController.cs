using AnimaLove.Helpers;
using AnimaLove.Models;
using AnimaLove.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public AccountController(UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser NewUser = new AppUser
            {
               Description=model.Description,
                Email = model.Email,
                UserName = model.UserName,
                FullName =model.FullName,
            };
            var IdentityResult =  await _userManager.CreateAsync(NewUser, model.Password);
            
            if (!IdentityResult.Succeeded)
            {
                foreach (var error in IdentityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRoleAsync(NewUser, Role.RoleType.Member.ToString());
           await _signInManager.SignInAsync(NewUser, true);
            return RedirectToAction("Index","Home");

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           AppUser DbUser= await _userManager.FindByEmailAsync(model.Email);
            if (DbUser==null)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(model);
            }
            AppUser newModel = new AppUser {
                Email = model.Email
            };
            var signInResult =
                 await _signInManager.PasswordSignInAsync(DbUser.UserName, model.Password, model.isPersistent,lockoutOnFailure: true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Prease try again later!");
                return View(model);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(model);

            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Role.RoleType)))
        //    {

        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
              
        //    }
            
        //}

    }
}

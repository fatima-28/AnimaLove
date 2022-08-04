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

        public AccountController(UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
           await _signInManager.SignInAsync(NewUser, true);
            return RedirectToAction("Index","Home");

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

    }
}

using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private AppDbContext _context { get; }
        public ProfileController(AppDbContext context, UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {


            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AppUser user = _userManager.FindByIdAsync(userId).Result;

                return View(user);
            }
           
        }
    }
}
using AnimaLove.DAL;
using AnimaLove.Helpers;
using AnimaLove.Models;
using AnimaLove.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Areas.AnimaLoveAdmin.Controllers
{
    [Authorize]
    [Area("AnimaLoveAdmin")]
    public class UserController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<AppUser> users;
        private IWebHostEnvironment _env { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(AppDbContext context,  IWebHostEnvironment env, UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            users = _context.Users.Where(u => !u.IsActivated);
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(users);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            AppUser userDb = _context.Users.Where(p => !p.IsActivated).FirstOrDefault(p => p.Id == Id);
            if (userDb == null)
            {
                return NotFound();
            }
            userDb.IsActivated = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser NewUser = new AppUser
            {
                Description = model.Description,
                Email = model.Email,
                UserName = model.UserName,
                FullName = model.FullName,
            };
            var IdentityResult = await _userManager.CreateAsync(NewUser, model.Password);

            if (!IdentityResult.Succeeded)
            {
                foreach (var error in IdentityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRoleAsync(NewUser, Role.RoleType.Member.ToString());
           
            return RedirectToAction("Index", "User");

        }
    }
}
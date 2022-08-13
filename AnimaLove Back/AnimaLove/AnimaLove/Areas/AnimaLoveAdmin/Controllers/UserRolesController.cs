using AnimaLove.DAL;
using AnimaLove.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Areas.AnimaLoveAdmin.Controllers
{
    [Authorize]
    [Area("AnimaLoveAdmin")]
    public class UserRolesController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<AppUser> users;
        private IWebHostEnvironment _env { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserRolesController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager,
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
    }
}

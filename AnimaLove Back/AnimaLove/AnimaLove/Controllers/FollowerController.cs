using AnimaLove.DAL;
using AnimaLove.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    [Authorize]
    public class FollowerController : Controller
    {
        private IWebHostEnvironment _env { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private AppDbContext _context { get; }
        
        public IEnumerable<AppUser> users;
        public FollowerController(AppDbContext context, UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            //users = _context.Users.Where(p => !p.IsActivated).ToList();
        }
        public IActionResult Index()
        {
            var userName = _userManager.FindByNameAsync(User.Identity.Name);
            var userID = userName.Result.Id;
            var users = _userManager.Users.Include(u => u.FollowerUser).Where(u=> u.Id==userID);
            

            return View(users);
        }
    }
}

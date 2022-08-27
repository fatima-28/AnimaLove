using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private AppDbContext _context { get; }
       
        private IWebHostEnvironment _env { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public MessageController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _context = context;
          
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            MessagesViewModel user = new MessagesViewModel
            {   users=_context.Users.Where(u=>!u.IsActivated).ToList()
                 

            };
            return View(user);
        }
    }
}

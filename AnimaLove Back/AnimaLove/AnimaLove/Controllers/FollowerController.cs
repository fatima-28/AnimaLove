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
            users = _context.Users.Where(p => !p.IsActivated).ToList();
        }
       
        public IActionResult GetFollowers(string Id)
        {

            List<string> FollowerList = new List<string>();
            var followerUser = _context.FollowerUser.Where(f => f.AppUserId == Id).ToList();
            foreach (var item in followerUser)
            {
                FollowerList.Add(item.FollowerId);

            }
            var followers = _context.Users.Where(u => FollowerList.Any(id => id == u.Id)).ToList();
            return View(followers);
          
        }
        //public IActionResult GetOthersProfile(string Id)
        //{

        //    return View();
        //}
        //public  IActionResult CreateFollower(string Id)
        //{
        //    if (Id==null)
        //    {
        //        return BadRequest();
        //    }

        //    FollowerList.Add(Id);
        //    var followers = _context.Users.Where(u => FollowerList.Any(id => id == u.Id)).ToList();
            
        //    return RedirectToAction(nameof(GetFollowers));

        //}


    }
}

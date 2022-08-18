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
    public class FollowingController : Controller
    {
        private IWebHostEnvironment _env { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private AppDbContext _context { get; }


        public IEnumerable<AppUser> users;
        public FollowingController(AppDbContext context, UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            users = _context.Users.Where(p => !p.IsActivated).ToList();
        }

        public IActionResult GetFollowings(string Id)
        {

            List<string> FollowingList = new List<string>();
            var followingUser = _context.FollowingUser.Where(f => f.AppUserId == Id).ToList();
            foreach (var item in followingUser)
            {
                FollowingList.Add(item.FollowingId);

            }
            var followers = _context.Users.Where(u => FollowingList.Any(id => id == u.Id)).ToList();
            return View(followers);

        }
        //public IActionResult GetOthersProfile(string Id, AppUser user)
        //{
        //    if (Id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var follower = _context.Users.Where(u => u.Id == Id).FirstOrDefault();


        //    return View(follower);
        //}



    }
}

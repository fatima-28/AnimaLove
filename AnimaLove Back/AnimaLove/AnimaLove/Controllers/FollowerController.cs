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
        
        static List<string> FollowingList = new List<string>();
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
        public IActionResult GetOthersProfile(string Id, AppUser user)
        {
            if (Id==null)
            {
                return BadRequest();
            }
            var follower = _context.Users.Where(u => u.Id == Id).FirstOrDefault();
           
            
            return View(follower);
        }
        public async Task< IActionResult> FollowAction(string Id)
        {

           
            var user = _context.Users.Where(w => w.Id == Id).FirstOrDefault(u => u.Id == Id);
            var userId = _userManager.GetUserId(HttpContext.User);
           
            FollowingUser newFollowingUser = new FollowingUser
            {
                FollowingId=Id,
                AppUserId= userId   

            };
           
          _context.FollowingUser.Add(newFollowingUser);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index","Profile");
            

            
        }



    }
}

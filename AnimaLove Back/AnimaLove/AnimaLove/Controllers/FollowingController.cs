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
            var followingUser = _context.FollowingUser.Where(f => f.AppUserId == Id).Where(u=>!u.IsDeleted).ToList();
            foreach (var item in followingUser)
            {
                FollowingList.Add(item.FollowingId);

            }
            var followers = _context.Users.Where(u => FollowingList.Any(id => id == u.Id)).ToList();
            return View(followers);

        }
        public async Task<IActionResult > UnfollowAction(string Id)
        {
            //var user = _context.FollowingUser.Where(u => u.FollowingId == Id).Where(u=>!u.IsDeleted).FirstOrDefault();
            var userId = _userManager.GetUserId(HttpContext.User);
            FollowingUser userDb = await _context.FollowingUser.Where(u =>!u.IsDeleted).FirstOrDefaultAsync(u=>u.FollowingId==Id);
            if (userDb==null)
            {
                return BadRequest();
            }
            userDb.IsDeleted = true;
          await  _context.SaveChangesAsync();
            //public async Task<IActionResult> Delete(int? id)
            //{
            //    if (id == null)
            //    {
            //        return BadRequest();
            //    }
            //    Category categoryDb = _context.Categories.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == id);
            //    if (categoryDb == null)
            //        return NotFound();

            //    categoryDb.IsDeleted = true;
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}


            //var followingUser = _context.Followings.Where(f => f.Id == Id).FirstOrDefault(f=>f.Id==Id);
            //  _context.Followings.Remove(followingUser);
            //_context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");



        }



    }
}

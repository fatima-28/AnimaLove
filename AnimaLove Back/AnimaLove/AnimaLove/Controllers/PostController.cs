using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels;
using AnimaLove.ViewModels.PostsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public IWebHostEnvironment _env { get; }
        private AppDbContext _context { get; }
        private readonly UserManager<AppUser> _userManager;
        public PostController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
               _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            PostViewModel home = new PostViewModel
            {
                Posts=_context.Posts.Where(p=>!p.IsDeleted).ToList()
            };
            return View(home);
        }
        public IActionResult GetPosts(string Id)
        {
            List<int> PostsList = new List<int>();
            var postDb = _context.Posts.Where(f => f.AppUserId == Id).ToList();
            foreach (var item in postDb)
            {
                PostsList.Add(item.Id);

            }
            var posts = _context.Posts.Where(u => PostsList.Any(Id => Id == u.Id)).ToList();
               return View(posts);



            //public IActionResult GetFollowers(string Id)
            //{
            //    List<string> FollowerList = new List<string>();

            //    var followerUser = _context.FollowerUser.Where(f => f.AppUserId == Id).ToList();
            //    foreach (var item in followerUser)
            //    {
            //        FollowerList.Add(item.FollowerId);

            //    }
            //    var followers = _context.Users.Where(u => FollowerList.Any(id => id == u.Id)).ToList();
            //    return View(followers);

            //}
        }
        public async Task< IActionResult >AddNewPost(CreatePostViewModel model, string Id)
        {
            var MyuserName = _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
           var UserId= MyuserName.Result.Id;
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                if (model.PostTitle == null)
                {
                    ModelState.AddModelError("PostTitle", "Name is required");
                    return View();
                }
                if (model.PostDescription == null)
                {
                    ModelState.AddModelError("PostDescription", "Description must be at least 10 character");
                    return View();
                }
                if (model.PostDescription.Length <= 10)
                {
                    ModelState.AddModelError("PostDescription", "Description must be at least 10 character");
                    return View();
                }
               

                Post post = new Post
                {
                    PostTitle = model.PostTitle,
                    PostDescription = model.PostDescription,
                    PostImage = uniqueFileName,
                    AppUserId=UserId,
                    userName=model.AppUser.UserName



                };
                MyuserName.Result.Posts.Add(post);


                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        private string UploadedFile(CreatePostViewModel model)
        {
            string uniqueFileName = null;

            if (model.PostPhoto != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "assets", "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PostPhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PostPhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

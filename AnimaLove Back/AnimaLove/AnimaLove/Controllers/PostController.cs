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
                Posts=_context.Posts.Where(p=>!p.IsDeleted).OrderByDescending(c=>c.Id).ToList()
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
            var posts = _context.Posts.Where(u => PostsList.Any(Id => Id == u.Id)).Where(p=>!p.IsDeleted).OrderByDescending(c => c.Id).ToList();
               return View(posts);

        }
        public IActionResult GetOthersPosts(string Id)
        {
            List<int> PostsList = new List<int>();
            var postDb = _context.Posts.Where(f => f.AppUserId == Id).ToList();
            foreach (var item in postDb)
            {
                PostsList.Add(item.Id);

            }
            var posts = _context.Posts.Where(u => PostsList.Any(Id => Id == u.Id)).Where(p => !p.IsDeleted).OrderByDescending(c => c.Id).ToList();
            return View(posts);



       
        }
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Post postDb = _context.Posts.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == Id);
            if (postDb == null)
            {
                return NotFound();
            }
            postDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult EditPost(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Post PostDb = _context.Posts.Where(w => !w.IsDeleted).FirstOrDefault(w => w.Id == Id);
            if (PostDb== null)
            {
                return NotFound();
            }
            return View(PostDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? Id, Post model)
        {

            if (ModelState.IsValid)
            {
                if (model.PostTitle == null)
                {
                    ModelState.AddModelError("PostTitle", "This Field is required");
                    return View();
                }
                if (model.PostDescription == null)
                {
                    ModelState.AddModelError("PostDescription", "This Field is required");
                    return View();
                }
                if (model.PostDescription.Length <= 10)
                {
                    ModelState.AddModelError("PostDescription", "Description must be at least 10 character");
                    return View();
                }

                var postDatabase = await _context.Posts.FindAsync(model.Id);

                postDatabase.PostTitle = model.PostTitle;
                postDatabase.PostDescription = model.PostDescription;


                if (model.PostPhoto != null)
                {
                    if (model.PostImage != null)
                    {
                        string filePath = Path.Combine(_env.WebRootPath, "assets", "img", model.PostImage);
                        System.IO.File.Delete(filePath);
                    }
                    postDatabase.PostImage = ProcessUploadedFile(model);

                }
                _context.Update(postDatabase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        public string ProcessUploadedFile(Post model)
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






        public IActionResult AddNewPost()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewPost(CreatePostViewModel model)
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var userId = user.Id;
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
                    AppUserId = userId,
                    userName=User.Identity.Name


                };
                _context.Add(post);
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

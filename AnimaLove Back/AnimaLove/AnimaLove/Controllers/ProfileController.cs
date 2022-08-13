using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels;
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
    public class ProfileController : Controller
    {

        private IWebHostEnvironment _env { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private AppDbContext _context { get; }
        public ProfileController(AppDbContext context, UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }
        public IActionResult Index()
        {

            var userName = _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (userName.Result.ProfileImage == null)
            {
                userName.Result.ProfileImage = "icon-img.png";
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AppUser user = _userManager.FindByIdAsync(userId).Result;

                return View(user);

            }
        }
        public IActionResult Edit(string Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            AppUser UserDb = _context.Users.Where(w => !w.IsActivated).FirstOrDefault(w => w.Id == Id);
            if (UserDb == null)
            {
                return NotFound();
            }
            return View(UserDb);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, AppUser model)
        {

            if (ModelState.IsValid)
            {
              
                if (model.FullName == null)
                {
                    ModelState.AddModelError("FullName", "This Field is required");
                    return View();
                }
                if (model.Description == null)
                {
                    ModelState.AddModelError("Description", "This Field is required");
                    return View();
                }
                if (model.Description.Length <= 10)
                {
                    ModelState.AddModelError("Description", "Description must be at least 10 character");
                    return View();
                }

                var userDatabase = await _context.Users.FindAsync(model.Id);

              
                userDatabase.Description = model.Description;
                userDatabase.FullName = model.FullName;


                if (model.ProfilePhoto != null)
                {
                    if (model.ProfileImage != null)
                    {
                        string filePath = Path.Combine(_env.WebRootPath, "assets", "img", model.ProfileImage);
                        System.IO.File.Delete(filePath);
                    }
                    userDatabase.ProfileImage = ProcessUploadedFile(model);

                }
                _context.Update(userDatabase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        public string ProcessUploadedFile(AppUser model)
        {
            string uniqueFileName = null;

            if (model.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "assets", "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePhoto.CopyTo(fileStream);
                }
            }

            return uniqueFileName;


        }

    }


}
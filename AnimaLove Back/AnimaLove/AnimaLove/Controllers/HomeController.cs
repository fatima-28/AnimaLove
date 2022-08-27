using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels;
using AnimaLove.ViewModels.PetViewModels;
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
  
    public class HomeController : Controller
    {
        public IWebHostEnvironment _env { get; }
       
        private readonly UserManager<AppUser> _userManager;
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Slides = _context.Slides.Where(s => !s.IsDeleted).ToList(),
                SlideSummaries= _context.SlideSummaries.Where(s => !s.IsDeleted).ToList(),
                Categories= _context.Categories.Where(c => !c.IsDeleted).ToList(),
                Pets= _context.Pets.Where(p => !p.IsAdopted).OrderByDescending(p=>p.Id).ToList(),
                Galleries= _context.Galleries.Where(p => !p.IsDeleted).ToList()

            };
            return View(home);
        }
        public IActionResult CreateAnnouncement()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnnouncement(CreatePetViewModel model)
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var userId=user.Id ;
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                if (model.Name == null)
                {
                    ModelState.AddModelError("Name", "Name is required");
                    return View();
                }
                if (model.Description == null)
                {
                    ModelState.AddModelError("Description", "Description must be at least 10 character");
                    return View();
                }
                if (model.Description.Length <= 10)
                {
                    ModelState.AddModelError("Description", "Description must be at least 10 character");
                    return View();
                }



                Pet pet = new Pet
                {
                    Name = model.Name,
                    Description = model.Description,
                    Image = uniqueFileName,
                    AppUserId= userId


                };
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private string UploadedFile(CreatePetViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "assets", "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}

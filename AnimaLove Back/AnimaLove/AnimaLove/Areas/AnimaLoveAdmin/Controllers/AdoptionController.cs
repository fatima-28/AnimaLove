
using AnimaLove.DAL;
using AnimaLove.Models;
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

namespace AnimaLove.Areas.AnimaLoveAdmin.Controllers
{
    [Authorize]
    [Area("AnimaLoveAdmin")]
    public class AdoptionController : Controller
    {
        private IWebHostEnvironment _env { get; }
        private AppDbContext _context { get; }
        public IEnumerable<Pet> pets;
        private readonly UserManager<AppUser> _userManager;
        public AdoptionController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            pets = _context.Pets.Where(p => !p.IsAdopted).ToList();

            _env = env;
        }
        public IActionResult Index()
        {
            return View(pets);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Pet petDb = _context.Pets.Where(p => !p.IsAdopted).FirstOrDefault(p => p.Id == Id);
            if (petDb == null)
            {
                return NotFound();
            }
            petDb.IsAdopted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePetViewModel model)
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var userId = user.Id;
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                if (model.Name==null)
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
        public IActionResult Update(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Pet PetDb = _context.Pets.Where(w => !w.IsAdopted).FirstOrDefault(w => w.Id == Id);
            if (PetDb == null)
            {
                return NotFound();
            }
            return View(PetDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Pet model)
        {

            if (ModelState.IsValid)
            {
                if (model.Name == null)
                {
                    ModelState.AddModelError("Name", "This Field is required");
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
             
                var petDatabase = await _context.Pets.FindAsync(model.Id);

                petDatabase.Name = model.Name;
                petDatabase.Description = model.Description;


                if (model.Photo != null)
                {
                    if (model.Image != null)
                    {
                        string filePath = Path.Combine(_env.WebRootPath, "assets", "img", model.Image);
                        System.IO.File.Delete(filePath);
                    }
                    petDatabase.Image = ProcessUploadedFile(model);

                }
                _context.Update(petDatabase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        public string ProcessUploadedFile(Pet model)
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

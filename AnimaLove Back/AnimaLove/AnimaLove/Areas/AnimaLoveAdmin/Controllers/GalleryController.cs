using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels.GalleryViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class GalleryController : Controller
    {
        private IWebHostEnvironment _env { get; }
        private AppDbContext _context { get; }
        public IEnumerable<Gallery> galleries;
        public GalleryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            galleries = _context.Galleries.Where(s => !s.IsDeleted).ToList();

            _env = env;
        }
        public IActionResult Index()
        {
            return View(galleries);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Gallery galleryDb = _context.Galleries.Where(s => !s.IsDeleted).FirstOrDefault(s => s.Id == Id);
            if (galleryDb == null)
            {
                return NotFound();
            }
            galleryDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGalleryVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);


                Gallery Gallery = new Gallery
                {

                    Image = uniqueFileName
                };
                _context.Add(Gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private string UploadedFile(CreateGalleryVM model)
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

using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels.SLiderViewModels;
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
    public class SliderController : Controller
    {
        private IWebHostEnvironment _env { get; }
        private AppDbContext _context { get; }
        public IEnumerable<Slide> slides;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            slides = _context.Slides.Where(s => !s.IsDeleted).ToList();

            _env = env;
        }
        public IActionResult Index()
        {
            return View(slides);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Slide slideDb = _context.Slides.Where(s => !s.IsDeleted).FirstOrDefault(s => s.Id == Id);
            if (slideDb == null)
            {
                return NotFound();
            }
            slideDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSliderViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);


                Slide slide = new Slide
                {

                    Image = uniqueFileName
                };
                _context.Add(slide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private string UploadedFile(CreateSliderViewModel model)
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
            Slide slideDb = _context.Slides.Where(s => !s.IsDeleted).FirstOrDefault(s => s.Id == Id);
            if (slideDb == null)
            {
                return NotFound();
            }
            return View(slideDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Slide model)
        {

            if (ModelState.IsValid)
            {

                var SlideDatabase = await _context.Slides.FindAsync(model.Id);




                if (model.Photo != null)
                {
                    if (model.Image != null)
                    {
                        string filePath = Path.Combine(_env.WebRootPath, "assets", "img", model.Image);
                        System.IO.File.Delete(filePath);
                    }
                    SlideDatabase.Image = ProcessUploadedFile(model);

                }
                _context.Update(SlideDatabase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        public string ProcessUploadedFile(Slide model)
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
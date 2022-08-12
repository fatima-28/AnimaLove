using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Areas.AnimaLoveAdmin.Controllers
{ [Authorize]
    [Area("AnimaLoveAdmin")]
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Category> categories;


        public CategoryController(AppDbContext context)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.IsDeleted);
        }

        public IActionResult Index()
        {
            return View(categories);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category categoryDb = _context.Categories.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == id);
            if (categoryDb == null)
                return NotFound();

            categoryDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = categories.Where(c => !c.IsDeleted).Any(c => c.Name.ToLower() == category.Name.ToLower());

            if (IsExist)
            {
                ModelState.AddModelError("Name", $"{category.Name} is exist");
                return View();
            }

            Category newCategory = new Category
            {

                Name = category.Name

            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }

            Category category = _context.Categories.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();

            }
            return View(category);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null)
                return BadRequest();


            Category categoryDb = _context.Categories.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == id);
            if (categoryDb == null)
                return NotFound();


            if (category.Name.ToLower() == categoryDb.Name.ToLower())
                return RedirectToAction(nameof(Index));
            bool IsExist = categories.Any(c => c.Name.ToLower() == category.Name.ToLower());

            if (IsExist)
            {
                ModelState.AddModelError("Name", $"{category.Name} is exist");
                return View();
            }

            categoryDb.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

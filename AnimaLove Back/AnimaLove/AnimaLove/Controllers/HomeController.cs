using AnimaLove.DAL;
using AnimaLove.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
  
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Slides = _context.Slides.Where(s => !s.IsDeleted).ToList(),
                SlideSummaries= _context.SlideSummaries.Where(s => !s.IsDeleted).ToList(),
                Categories= _context.Categories.Where(c => !c.IsDeleted).ToList(),
                Pets= _context.Pets.Where(p => !p.IsAdopted).ToList(),
                Galleries= _context.Galleries.Where(p => !p.IsDeleted).ToList()

            };
            return View(home);
        }
    }
}

using AnimaLove.DAL;
using AnimaLove.ViewModels;
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
                SlideSummaries= _context.SlideSummaries.Where(s => !s.IsDeleted).ToList()

            };
            return View(home);
        }
    }
}

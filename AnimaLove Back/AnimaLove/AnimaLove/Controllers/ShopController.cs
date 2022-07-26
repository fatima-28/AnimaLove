using AnimaLove.DAL;
using AnimaLove.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _context { get; }
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ShopViewModel shop = new ShopViewModel
            {

                Categories = _context.Categories.Where(c => !c.IsDeleted).ToList(),
                Products = _context.Products.Where(p => !p.IsDeleted).ToList()
               

            };
            return View(shop);
        }
    }
}

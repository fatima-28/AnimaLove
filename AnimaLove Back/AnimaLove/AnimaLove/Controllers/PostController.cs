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
    [Authorize]
    public class PostController : Controller
    {
        private AppDbContext _context { get; }
        public PostController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            PostViewModel home = new PostViewModel
            {
                Posts=_context.Posts.Where(p=>!p.IsDeleted).ToList()
            };
            return View(home);
        }
    }
}

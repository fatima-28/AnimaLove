using AnimaLove.DAL;
using AnimaLove.Models;
using AnimaLove.ViewModels;
using AnimaLove.ViewModels.PostsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Controllers
{
    [Authorize]
    public class FullPostInfoController : Controller
    {
        private IWebHostEnvironment _env { get; }
       
        private AppDbContext _context { get; }
        public FullPostInfoController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public  IActionResult Index(int? Id, Post model)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            var post = _context.Posts.Where(p=> !p.IsDeleted).FirstOrDefault(p=>p.Id==Id);


            return View(post);

        }
        public IActionResult Others(int? Id, Post model)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            var post = _context.Posts.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == Id);


            return View(post);

        }
    }
}

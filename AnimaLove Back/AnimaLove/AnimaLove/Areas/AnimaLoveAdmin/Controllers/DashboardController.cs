using AnimaLove.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Areas.AnimaLoveAdmin.Controllers
{
    [Authorize(Roles =nameof(Role.RoleType.Admin))]
    [Area("AnimaLoveAdmin")]
    
   
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

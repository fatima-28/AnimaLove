using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public Post Posts { get; set; }
        public bool IsActivated { get; set; }
       
     
    }
}

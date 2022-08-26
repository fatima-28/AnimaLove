using AnimaLove.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.ViewModels.PostsViewModels
{
    public class SpesificPostVM
    {
        
        
        public string userName { get; set; }
        public string PostImage { get; set; }
        [Required,NotMapped]
        public IFormFile PostPhoto { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
       
        public bool IsDeleted { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

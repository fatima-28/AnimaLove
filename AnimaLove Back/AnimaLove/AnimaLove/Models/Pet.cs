﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped,Required]
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public bool IsAdopted { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

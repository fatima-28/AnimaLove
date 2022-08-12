using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.ViewModels.SLiderViewModels
{
    public class CreateSliderViewModel
    {
        public string Image { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
    }
}

using AnimaLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }
        public List<SlideSummary> SlideSummaries { get; set; }
        public List<Category> Categories { get; set; }
        public List<Pet> Pets { get; set; }
        public List<Gallery> Galleries { get; set; }
    }
}

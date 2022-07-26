using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsAdopted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

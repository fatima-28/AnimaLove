using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string PostImage { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public int LikeCount { get; set; }
        public bool IsDeleted { get; set; }

    }
}

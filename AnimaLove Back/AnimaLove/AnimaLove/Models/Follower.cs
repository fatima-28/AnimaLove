using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class Follower
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public ICollection<FollowerUser> FollowerUser { get; set; }
    }
}

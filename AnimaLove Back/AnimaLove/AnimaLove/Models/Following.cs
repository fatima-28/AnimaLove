using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class Following
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public ICollection<FollowingUser> FollowingUser { get; set; }
    }
}

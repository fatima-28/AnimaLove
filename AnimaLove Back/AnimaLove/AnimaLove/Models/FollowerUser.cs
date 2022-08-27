using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class FollowerUser
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Follower Follower { get; set; }
        public bool IsDeleted { get; set; }
    }
}

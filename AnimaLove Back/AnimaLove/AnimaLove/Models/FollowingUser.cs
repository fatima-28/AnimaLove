using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.Models
{
    public class FollowingUser
    {
        public int Id { get; set; }
        public string FollowingId { get; set; }
        public string AppUserId { get; set; }
    }
}

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
        public bool IsDeleted { get; set; }
        //public AppUser AppUser { get; set; }
        //public Following Following { get; set; }
    }
}

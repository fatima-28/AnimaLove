using AnimaLove.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SlideSummary> SlideSummaries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<AppUser> Profiles { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<FollowerUser> FollowerUser { get; set; }
        public DbSet<FollowingUser> FollowingUser { get; set; }

    }
}

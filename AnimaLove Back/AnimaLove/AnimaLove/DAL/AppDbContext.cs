using AnimaLove.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimaLove.DAL
{
    public class AppDbContext:DbContext
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

    }
}

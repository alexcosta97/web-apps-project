using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Address> Address { get; set; }

        public DbSet<Favourite> Favourite { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<RouteStop> RouteStop { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Stop> Stop { get; set; }
    }
}

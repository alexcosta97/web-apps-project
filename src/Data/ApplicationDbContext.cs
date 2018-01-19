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

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Favourite> Favourites { get; set; }
		public DbSet<Line> Lines { get; set; }
		public DbSet<Route> Routes { get; set; }
		public DbSet<RouteStop> RouteStops { get; set; }
		public DbSet<Staff> Staff { get; set; }
		public DbSet<Stop> Stops { get; set; }
	}
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace src.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		public ICollection<Favourite> Favourites { get; set; }
		public ICollection<Address> Addresses { get; set; }
	}
}

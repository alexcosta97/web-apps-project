using System.ComponentModel.DataAnnotations;

namespace src.Models
{
	public class Favourite
	{
		public int FavouriteID { get; set; }

		[Display(Name = "User ID")]
		public string ApplicationUserID { get; set; }

		public string Name { get; set; }

		[Display(Name = "Route ID")]
		public int? RouteID { get; set; }

		[Display(Name = "Line ID")]
		public int? LineID { get; set; }

		[Display(Name = "Route")]
		public Route route { get; set; }

		public Line Line { get; set; }

		[Display(Name = "Application User")]
		public ApplicationUser ApplicationUser { get; set; }
	}
}

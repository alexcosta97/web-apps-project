using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
	public enum Direction
	{
		[Display(Name = "Inbound")]
		Inbound,

		[Display(Name = "Outbound")]
		Outbound
	}

	public class Route
	{
		public int RouteID { get; set; }

		[Display(Name = "Driver ID")]
		public string DriverID { get; set; }

		[Display(Name = "Line ID")]
		public int LineID { get; set; }

		public string Name { get; set; }
		public string Note { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime Departure { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime Arrival { get; set; }

		public Staff Driver { get; set; }

		public Direction Direction { get; set; }
		public ICollection<Stop> Stops { get; set; }
	}
}

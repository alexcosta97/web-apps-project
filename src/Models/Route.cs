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
        public int Id{get;set;}
        public string Name{get;set;}
        public string Note{get;set;}
        public DateTime Departure{get;set;}
        public DateTime Arrival{get;set;}

        public Staff Driver{get;set;}
        public Line Line{get;set;}

        public Direction Direction{get;set;}

        public ICollection<Stop> Stops{get;set;}
    }
}

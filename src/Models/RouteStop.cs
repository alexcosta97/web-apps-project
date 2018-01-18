using System;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class RouteStop
    {
        public int RouteStopID{get;set;}
        [Display(Name = "Route ID")]
        public int RouteID{get;set;}
        [Display(Name = "Stop ID")]
        public int StopID{get;set;}

        public Route Route{get;set;}
        public Stop Stop{get;set;}
    }
}
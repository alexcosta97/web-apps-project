using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Line
    {
        public int Id{get;set;}
        public string Name{get;set;}
        [DataType(DataType.Date)]
        public DateTime Start{get;set;}
        [DataType(DataType.Date)]
        public DateTime End{get;set;}

        public ICollection<Route> Routes{get;set;}
    }
}
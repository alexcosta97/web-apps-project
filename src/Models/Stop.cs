using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Stop
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public double Longitude{get;set;}
        public double Latitude{get;set;}
    }
}
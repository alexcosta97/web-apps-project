using System;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Stop
    {
        public int StopID{get;set;}
        public string Name{get;set;}
        public string Longitude{get;set;}
        public string Latitude{get;set;}
    }
}
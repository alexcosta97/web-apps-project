using System;

namespace src.Models
{
    public class RouteStop
    {
        public int Id{get;set;}
        public int RouteID{get;set;}
        
        public int StopID{get;set;}
        public Route Route{get;set;}
        public Stop Stop{get;set;}
    }
}
using System;

namespace src.Models
{
    public class RouteStop
    {
        public int Id{get;set;}
        public Route Route{get;set;}
        public Stop Stop{get;set;}
    }
}
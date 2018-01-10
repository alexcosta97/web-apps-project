using System;
using System.Collections.Generic;

namespace src.Models
{
    public class Favourite
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public Route Route{get;set;}
        public Line Line{get;set;}
    }
}
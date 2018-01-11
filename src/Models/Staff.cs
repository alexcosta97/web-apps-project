using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace src.Models
{
    public class Staff
    {
        public int Id{get;set;}
        public string ownerID {get;set;}
        public int hoursContracted{get;set;}
        public string accountNumber{get;set;}
        public string sortCode{get;set;}
        public string nationalInsuranceNumber{get;set;}

        public ApplicationUser appUser{get;set;}
    }
}
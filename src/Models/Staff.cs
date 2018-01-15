using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Staff
    {
        public int Id {get;set;}
        [Display(Name="Manager ID")]
        public string ownerID {get;set;}
        [Display(Name="Hours Contracted")]
        public int hoursContracted{get;set;}
        [Display(Name="Account Number")]
        public string accountNumber{get;set;}
        [Display(Name="Sort Code")]
        public string sortCode{get;set;}
        [Display(Name="National Insurance Number")]
        public string nationalInsuranceNumber{get;set;}

        [Display(Name="App User")]
        public ApplicationUser appUser{get;set;}
    }
}
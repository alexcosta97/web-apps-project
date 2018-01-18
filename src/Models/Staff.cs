using System;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Staff
    {
        public int StaffID{get;set;}
        [Display(Name="User ID")]
        public string ApplicationUserID{get;set;}

        [Display(Name="Hours Contracted")]
        public int hoursContracted{get;set;}
        [Display(Name="Account Number")]
        public string accountNumber{get;set;}
        [Display(Name="Sort Code")]
        public string sortCode{get;set;}

        [Display(Name = "National Insurance Number")]
        public string nationalInsuranceNumber{get;set;}

        [Display(Name="Application User")]
        public ApplicationUser ApplicationUser{get;set;}
    }
}
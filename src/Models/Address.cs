using System;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Address
    {
        public int AddressID{get;set;}
        [Display(Name= "County")]
        public string county{get;set;}
        [Display(Name = "Post Code")]
        public string postCode{get;set;}
        [Display(Name = "Street 1")]
        public string street1{get;set;}
        [Display(Name = "Street 2")]
        public string street2{get;set;}
        public string ApplicationUserID{get;set;}

        public ApplicationUser ApplicationUser{get;set;}
    }
}
using System;

namespace src.Models
{
    public class Address
    {
        public string county{get;set;}
        public string postCode {get;set;}
        public string street1{get;set;}
        public string street2{get;set;}

        public ApplicationUser ApplicationUser{get;set;}
    }
}

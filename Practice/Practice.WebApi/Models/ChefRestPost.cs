using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice.WebApi.Models
{
    public class ChefRestPost
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber {  get; set; }
        public string HomeAddress { get; set; }
        public string OIB { get; set; }
        public DateTime HireDate { get; set; }
       
    }
}
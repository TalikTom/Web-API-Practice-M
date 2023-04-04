using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice.MVC.Models
{
    public class ChefDetailsView
    {
        [Key]
        public Guid Id { get; set; }
    
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string HomeAddress { get; set; }

        public bool Certified { get; set; }

        public string OIB { get; set; }

        public DateTime HireDate { get; set; }
    }
}
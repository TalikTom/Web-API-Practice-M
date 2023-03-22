using Practice.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Practice.WebApi.Models
{
    public class ChefModel
    {

        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string HomeAddress { get; set; }

        public bool Certified { get; set; }

        public string OIB { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime StartDate { get; set; }


    }
}
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
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime StartDate { get; set; }

        public int Id { get; set; }

        public bool Certified { get; set; }


    }
}
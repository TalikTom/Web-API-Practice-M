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

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string HomeAddress { get; set; }

        [Required]
        public bool Certified { get; set; }

        [Required]
        public string OIB { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyyTHH:mm}")]
        public DateTime HireDate { get; set; }

        public List<OrderView> Orders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice.MVC.Models
{
    public class OrderView
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ChefId { get; set; }
        
    }
}
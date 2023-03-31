using Practice.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Model
{
    public class ChefModelDTO : IChefModel
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



    }
}

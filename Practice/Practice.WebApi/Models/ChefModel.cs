using Practice.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice.WebApi.Models
{
    public class ChefModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartDate { get; set; }

        public int Id { get; set; }

        public bool Certified { get; set; }

        public List<ChefModel> chefs { get; set; }

        public List<ChefModel> GetAllChefs()
        {
            return chefs;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;

namespace Practice.WebApi.Models
{
    public class ChefRestGet
    {

       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }

    }
}
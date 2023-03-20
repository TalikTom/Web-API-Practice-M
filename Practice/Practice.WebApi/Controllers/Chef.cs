using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice.WebApi.Controllers
{
    public class Chef
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartDate { get; set; }

        public Guid Id { get; set; }

        public bool Certified { get; set; }
    }
}
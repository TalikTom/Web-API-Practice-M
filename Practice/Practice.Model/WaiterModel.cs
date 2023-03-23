using Practice.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Model
{
    public class WaiterModel : IWaiterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartDate { get; set; }

        public int Id { get; set; }

        public bool Certified { get; set; }

    }
}

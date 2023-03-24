using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Model.Common
{
    public interface IWaiterModel
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        DateTime StartDate { get; set; }

        int Id { get; set; }

        bool Certified { get; set; }

    }
}

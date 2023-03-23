using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Model.Common
{
    public interface IChefModel
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string HomeAddress { get; set; }
        bool Certified { get; set; }
        string OIB { get; set; }
        DateTime HireDate { get; set; }
    }
}

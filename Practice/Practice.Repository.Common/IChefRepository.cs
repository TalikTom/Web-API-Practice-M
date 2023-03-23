using Practice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Repository.Common
{
    public interface IChefRepository
    {
        List<ChefModel> GetAll();
        ChefModel Get(Guid id);
    }
}

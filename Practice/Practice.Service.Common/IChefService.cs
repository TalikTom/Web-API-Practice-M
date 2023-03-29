using Practice.Common;
using Practice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Service.Common
{
    public interface IChefService
    {
        Task<List<ChefModel>> GetAllAsync(Paging paging);

        Task<ChefModel> GetAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<ChefModel> PostAsync(ChefModel chef);

        Task<bool> PutAsync(Guid id, ChefModel chef);
    }
}

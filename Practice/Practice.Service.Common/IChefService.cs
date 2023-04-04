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
        Task<List<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef, SearchString search);

        Task<ChefModelDTO> GetByIdAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<ChefModelDTO> PostAsync(ChefModelDTO chef);

        Task<int> PostRandomChefsAsync(int count);

        Task<bool> PutAsync(Guid id, ChefModelDTO chef);
    }
}

using PagedList;
using Practice.Common;
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
        Task<IPagedList<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef);

        Task<ChefModelDTO> GetByIdAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<ChefModelDTO> PostAsync(ChefModelDTO chef);

        Task<int> PostRandomChefsAsync(int count);

        Task<bool> PutAsync(Guid id, ChefModelDTO chef);
    }
}

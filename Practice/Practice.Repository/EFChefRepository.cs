using Practice.Common;
using Practice.Model;
using Practice.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Repository
{
    public class EFChefRepository : IChefRepository
    {

        public EFChefRepository()
        {

        }
        public Task<bool> DeleteAsync(Guid id)
        {
            
        }

        public Task<List<ChefModel>> GetAllAsync(Paging paging, Sorting sorting, ChefFilter filteringChef)
        {
            
        }

        public Task<ChefModel> GetAsync(Guid id)
        {
            
        }

        public Task<ChefModel> PostAsync(ChefModel chef)
        {
            
        }

        public Task<bool> PutAsync(Guid id, ChefModel chef)
        {
            
        }
    }
}

using Practice.Model;
using Practice.Repository;
using Practice.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Service
{
    public class ChefService : IChefService
    {

        public async Task<List<ChefModel>> GetAllAsync()
        {
            ChefRepository chefRepository = new ChefRepository();
            List<ChefModel> chefs = await chefRepository.GetAllAsync();

            return chefs;
        }


        public async Task<ChefModel> GetAsync(Guid id)
        {
            ChefRepository chefRepository = new ChefRepository();
            ChefModel chef = await chefRepository.GetAsync(id);

            return chef;
        }


        public async Task<ChefModel> PostAsync(ChefModel chef)
        {
            ChefRepository chefRepository = new ChefRepository();
            chef = await chefRepository.PostAsync(chef);

            return chef;
        }


        public async Task<bool> PutAsync(Guid id, ChefModel chef)
        {
          

            ChefRepository chefRepository = new ChefRepository();

            ChefModel chefExist = await chefRepository.GetAsync(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chefCheck = await chefRepository.PutAsync(id, chef);

            return chefCheck;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            
            ChefRepository chefRepository = new ChefRepository();

            ChefModel chefExist = await chefRepository.GetAsync(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chef = await chefRepository.DeleteAsync(id);

            return chef;
        }
    }
}

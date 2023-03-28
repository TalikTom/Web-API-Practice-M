using Practice.Model;
using Practice.Repository;
using Practice.Repository.Common;
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

        protected IChefRepository ChefRepository;

        public ChefService(IChefRepository chefRepository)
        {
            ChefRepository = chefRepository;
        }

        public async Task<List<ChefModel>> GetAllAsync()
        {
           
            List<ChefModel> chefs = await ChefRepository.GetAllAsync();

            return chefs;
        }


        public async Task<ChefModel> GetAsync(Guid id)
        {
            
            ChefModel chef = await ChefRepository.GetAsync(id);

            return chef;
        }


        public async Task<ChefModel> PostAsync(ChefModel chef)
        {
           
            chef = await ChefRepository.PostAsync(chef);

            return chef;
        }


        public async Task<bool> PutAsync(Guid id, ChefModel chef)
        {
          

            

            ChefModel chefExist = await ChefRepository.GetAsync(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chefCheck = await ChefRepository.PutAsync(id, chef);

            return chefCheck;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            
            

            ChefModel chefExist = await ChefRepository.GetAsync(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chef = await ChefRepository.DeleteAsync(id);

            return chef;
        }
    }
}

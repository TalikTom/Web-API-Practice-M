using Practice.Common;
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

        public async Task<List<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef)
        {
           
            List<ChefModelDTO> chefs = await ChefRepository.FindAsync(paging, sorting, filteringChef);

            return chefs;
        }


        public async Task<ChefModelDTO> GetByIdAsync(Guid id)
        {

            ChefModelDTO chef = await ChefRepository.GetByIdAsync(id);

            return chef;
        }


        public async Task<ChefModelDTO> PostAsync(ChefModelDTO chef)
        {

            chef = await ChefRepository.PostAsync(chef);

            return chef;
        }

        public async Task<int> PostRandomChefsAsync(int count)
        {

            count = await ChefRepository.PostRandomChefsAsync(count);

            return count;
        }


        public async Task<bool> PutAsync(Guid id, ChefModelDTO chef)
        {




            ChefModelDTO chefExist = await ChefRepository.GetByIdAsync(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chefCheck = await ChefRepository.PutAsync(id, chef);

            return chefCheck;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {



            ChefModelDTO chefExist = await ChefRepository.GetByIdAsync(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chef = await ChefRepository.DeleteAsync(id);

            return chef;
        }
    }
}

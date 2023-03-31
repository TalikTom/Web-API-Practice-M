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

        protected IWaiterService WaiterService;

        public ChefService(IChefRepository chefRepository)
        {
            ChefRepository = chefRepository;
        }

        public async Task<List<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef)
        {
           
            List<ChefModelDTO> chefs = await ChefRepository.FindAsync(paging, sorting, filteringChef);

          
            return chefs;
        }


        //public async Task<ChefModel> GetByIdAsync(Guid id)
        //{

        //    ChefModel chef = await ChefRepository.GetByIdAsync(id);

        //    return chef;
        //}


        //public async Task<ChefModel> PostAsync(ChefModel chef)
        //{

        //    chef = await ChefRepository.PostAsync(chef);

        //    return chef;
        //}


        //public async Task<bool> PutAsync(Guid id, ChefModel chef)
        //{




        //    ChefModel chefExist = await ChefRepository.GetAsync(id);

        //    if (chefExist == null)
        //    {
        //        return false;
        //    }

        //    bool chefCheck = await ChefRepository.PutAsync(id, chef);

        //    return chefCheck;
        //}


        //public async task<bool> deleteasync(guid id)
        //{



        //    chefmodel chefexist = await chefrepository.getasync(id);

        //    if (chefexist == null)
        //    {
        //        return false;
        //    }

        //    bool chef = await chefrepository.deleteasync(id);

        //    return chef;
        //}
    }
}

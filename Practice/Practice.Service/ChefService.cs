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

        public List<ChefModel> GetAll()
        {
            ChefRepository chefRepository = new ChefRepository();
            List<ChefModel> chefs = chefRepository.GetAll();

            return chefs;
        }


        public ChefModel Get(Guid id)
        {
            ChefRepository chefRepository = new ChefRepository();
            ChefModel chef = chefRepository.Get(id);

            return chef;
        }


        public ChefModel Post(ChefModel chef)
        {
            ChefRepository chefRepository = new ChefRepository();
            chef = chefRepository.Post(chef);

            return chef;
        }


        public bool Put(Guid id, ChefModel chef)
        {
          

            ChefRepository chefRepository = new ChefRepository();

            ChefModel chefExist = chefRepository.Get(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chefCheck = chefRepository.Put(id, chef);

            return chefCheck;
        }


        public bool Delete(Guid id)
        {
            
            ChefRepository chefRepository = new ChefRepository();

            ChefModel chefExist = chefRepository.Get(id);

            if (chefExist == null)
            {
                return false;
            }

            bool chef = chefRepository.Delete(id);

            return chef;
        }
    }
}

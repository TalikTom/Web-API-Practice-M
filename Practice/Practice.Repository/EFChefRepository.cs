using Practice.Common;
using Practice.Dal;
using Practice.Model;
using Practice.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Repository
{
    public class EFChefRepository : IChefRepository
    {
        protected RestaurantContext DbContext;

        public EFChefRepository(RestaurantContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<ChefModel>> GetAllAsync(Paging paging, Sorting sorting, ChefFilter filteringChef)
        {
            var query = DbContext.Chef.AsQueryable();

            if (!string.IsNullOrEmpty(filteringChef.FirstName))
            {
                query = query.Where(chef => chef.FirstName.Contains(filteringChef.FirstName));
            }

            if (!string.IsNullOrEmpty(filteringChef.LastName))
            {
                query = query.Where(chef => chef.LastName.Contains(filteringChef.LastName));
            }

            if (filteringChef.HireDate.HasValue)
            {
                query = query.Where(chef => chef.HireDate == filteringChef.HireDate.Value);
            }

            //paging
           
            //sorting


            var chefs = await query.ToListAsync();

            if (chefs.Count == 0)
            {
                return null;
            }

            var chefModels = chefs.Select(chef => new ChefModel
            {
                Id = chef.Id,
                FirstName = chef.FirstName,
                LastName = chef.LastName,
                PhoneNumber = chef.PhoneNumber,
                HomeAddress = chef.HomeAddress,
                Certified = chef.Certified,
                OIB = chef.OIB,
                HireDate = chef.HireDate
            }).ToList();

            return chefModels;
        }

        //public Task<bool> DeleteAsync(Guid id)
        //{

        //}



        //public Task<ChefModel> GetAsync(Guid id)
        //{

        //}

        //public Task<ChefModel> PostAsync(ChefModel chef)
        //{

        //}

        //public Task<bool> PutAsync(Guid id, ChefModel chef)
        //{

        //}
    }
}

using Practice.Common;
using Practice.Dal;
using Practice.Model;
using Practice.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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


        public async Task<List<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef)
        {

            IQueryable<Chef> query = DbContext.Chef.AsQueryable();


            if (filteringChef != null)
            {

                if (!string.IsNullOrEmpty(filteringChef.FirstName))
                {
                    query = query.Where(chef => chef.FirstName.ToLower().Contains(filteringChef.FirstName.ToLower()));

                }

                if (!string.IsNullOrEmpty(filteringChef.LastName))
                {
                    query = query.Where(chef => chef.LastName.ToLower().Contains(filteringChef.LastName.ToLower()));

                }

                if (filteringChef.HireDate.HasValue)
                {
                    query = query.Where(chef => chef.HireDate == filteringChef.HireDate.Value);
                }

            }


            if (paging != null)
            {
                int offset = (paging.Page - 1) * paging.ItemsPerPage;
                int fetchNext = paging.ItemsPerPage;

                query = query.OrderBy(x => x.Id).Skip(offset).Take(fetchNext);
            }


            if (sorting != null)
            {
                string sortBy = sorting.SortBy;
                string sortOrder = sorting.SortOrder;

                switch (sortBy.ToLower())
                {
                    case "firstname":
                        query = sortOrder.ToLower() == "desc"
                            ? query.OrderByDescending(chef => chef.FirstName)
                            : query.OrderBy(chef => chef.FirstName);
                        break;
                    case "lastname":
                        query = sortOrder.ToLower() == "desc"
                            ? query.OrderByDescending(chef => chef.LastName)
                            : query.OrderBy(chef => chef.LastName);
                        break;
                    case "hiredate":
                        query = sortOrder.ToLower() == "desc"
                            ? query.OrderByDescending(chef => chef.HireDate)
                            : query.OrderBy(chef => chef.HireDate);
                        break;
                    default:

                        query = sortOrder.ToLower() == "desc"
                            ? query.OrderByDescending(chef => chef.Id)
                            : query.OrderBy(chef => chef.Id);
                        break;
                }
            }



            List<Chef> chefs = await query.ToListAsync();

            if (chefs.Count == 0)
            {
                return null;
            }

            List<ChefModelDTO> chefModels = chefs.Select(chef => new ChefModelDTO
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


        public async Task<ChefModelDTO> GetByIdAsync(Guid id)
        {
            ChefModelDTO query = await DbContext.Chef.AsQueryable()

                   .Where(c => c.Id == id)
                   .Select(c => new ChefModelDTO
                   {
                       Id = c.Id,
                       FirstName = c.FirstName,
                       LastName = c.LastName,
                       PhoneNumber = c.PhoneNumber,
                       HomeAddress = c.HomeAddress,
                       Certified = c.Certified,
                       OIB = c.OIB,
                       HireDate = c.HireDate,
                       CustomerOrder = c.CustomerOrder
                               .Where(co => co.ChefId == c.Id)
                               .Select(co => new Practice.Model.CustomerOrder
                               {
                                   Id = co.Id,
                                   ChefId = co.ChefId
                               }).ToList()
                   })
                   .FirstOrDefaultAsync();

            return query;

            ////Read raw sql from .txt file

            //string file = @"C:\Users\student\Documents\Luka\rawsql.txt";

            //var sql = File.ReadAllText(file);
            //var parameters = new SqlParameter[] { new SqlParameter("@id", id) };
            //var chef = await DbContext.Database.SqlQuery<ChefModelDTO>(sql, parameters).FirstOrDefaultAsync();

            //return chef;

        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            IQueryable<Chef> query = DbContext.Chef.AsQueryable();

            Chef chef = await query.FirstOrDefaultAsync(c => c.Id == id);

            if (chef != null)
            {
                DbContext.Chef.Remove(chef);
                await DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }



        public async Task<ChefModelDTO> PostAsync(ChefModelDTO chef)
        {
            Chef newChef = new Chef
            {
                Id = Guid.NewGuid(),
                FirstName = chef.FirstName,
                LastName = chef.LastName,
                PhoneNumber = chef.PhoneNumber,
                HomeAddress = chef.HomeAddress,
                Certified = chef.Certified,
                OIB = chef.OIB,
                HireDate = chef.HireDate
            };


            DbContext.Chef.Add(newChef);


            int rowsAffected = await DbContext.SaveChangesAsync();


            if (rowsAffected > 0)
            {
                chef.Id = newChef.Id;
                return chef;
            }

            return null;
        }

        public async Task<bool> PutAsync(Guid id, ChefModelDTO chef)
        {
            IQueryable<Chef> query = DbContext.Chef.AsQueryable();

            Chef existingChef = await query.FirstOrDefaultAsync(c => c.Id == id);

            if (existingChef != null)
            {
                
                existingChef.FirstName = chef.FirstName;
                existingChef.LastName = chef.LastName;
                existingChef.PhoneNumber = chef.PhoneNumber;
                existingChef.HomeAddress = chef.HomeAddress;
                existingChef.Certified = chef.Certified;
                existingChef.OIB = chef.OIB;
                existingChef.HireDate = chef.HireDate;

                await DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> PostRandomChefsAsync(int count)
        {
            List<ChefModelDTO> randomChefs = GenerateRandom.GenerateRandomChefs(count);

            foreach (ChefModelDTO chef in randomChefs)
            {
                await PostAsync(chef);
            }

            return count;
        }

              
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Razor.Generator;
using Practice.WebApi.Models;



namespace Practice.WebApi.Controllers
{
    public class ChefController : ApiController
    {

        public static List<ChefModel> chefs = new List<ChefModel>()
        {
               new ChefModel { FirstName = "Djordje", LastName = "Balasevic", StartDate = DateTime.Now, Id = 1, Certified = false},
               new ChefModel { FirstName = "Ciro", LastName = "Gasparac", StartDate = DateTime.Now, Id = 2, Certified = true},
               new ChefModel { FirstName = "Maksim", LastName = "Mrvica", StartDate = DateTime.Now, Id = 3, Certified = false},
               new ChefModel { FirstName = "Himzo", LastName = "Polovina", StartDate = DateTime.Now, Id = 4, Certified = true},
        };

        // GET home/chef
        public List<ChefModel> Get()
        {
            return chefs.Select(c => new ChefModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                StartDate = c.StartDate,
                Certified = c.Certified,
                Id = c.Id
            }).ToList<ChefModel>();
        }

        // GET home/waiter/5
        public ChefModel Get(int id)
        {
            return chefs.FirstOrDefault(c => c.Id == id);
        }

        // POST home/waiter
        // Use [FromUri] attribute to force Web API to post the value of complex type from the query string
        // Example of URI:
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool
        public List<ChefModel> Post([FromUri] ChefModel chef)
        {

            chef.Id = chefs.Count + 1;
            chef.StartDate = DateTime.Now;
            chefs.Add(chef);

            return Get();


        }

        // PUT home/waiter/5
        // Use [FromUri] attribute to force Web API to update the value of complex type from the query string
        // Example of URI:
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool
        public List<ChefModel> Put(int id, [FromUri] ChefModel chef)
        {
            ChefModel chefToUpdate = chefs.FirstOrDefault(c => c.Id == id);

            chefToUpdate.FirstName = chef.FirstName;
            chefToUpdate.LastName = chef.LastName;
            chefToUpdate.StartDate = chef.StartDate;
            chefToUpdate.Certified = chef.Certified;

            return Get();

        }

        // DELETE home/waiter/5
        public List<ChefModel> Delete(int id)
        {
            ChefModel chefToRemove = chefs.FirstOrDefault(c => c.Id == id);

            chefs.Remove(chefToRemove);

            return Get();
        }


    }
}
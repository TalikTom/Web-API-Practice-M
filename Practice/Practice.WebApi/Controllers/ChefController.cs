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
               new ChefModel { FirstName = "Djordje", LastName = "Balasevic", StartDate = new DateTime(2022, 1, 1, 10, 2, 0), Id = 1, Certified = false},
               new ChefModel { FirstName = "Ciro", LastName = "Gasparac", StartDate = new DateTime(2022, 1, 1, 15, 45, 0), Id = 2, Certified = true},
               new ChefModel { FirstName = "Maksim", LastName = "Mrvica", StartDate = new DateTime(2022, 1, 5, 1, 0, 0), Id = 3, Certified = false},
               new ChefModel { FirstName = "Himzo", LastName = "Polovina", StartDate = new DateTime(2021, 12, 1, 10, 0, 30), Id = 4, Certified = true},
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
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool&startDate=2022-03-21T12:00:00Z
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
        // Use[FromBody] attribute to force Web API to delete the value of primitive type from the body
        // Example of Body:
        // 2
        public List<ChefModel> Delete([FromBody] int id)
        {
            ChefModel chefToRemove = chefs.FirstOrDefault(c => c.Id == id);

            chefs.Remove(chefToRemove);

            return Get();
        }


    }
}
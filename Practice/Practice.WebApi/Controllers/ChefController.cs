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

        List<ChefModel> chefs = null;

        public ChefController()
        {
            chefs = new List<ChefModel>();

            ChefModel chef = new ChefModel();
            chef.FirstName = "Marko";
            chef.LastName = "Marulic";
            chef.StartDate = DateTime.Now;
            chef.Certified = false;
            chef.Id = 1;
            chefs.Add(chef);

            chef = new ChefModel();
            chef.FirstName = "Ivan";
            chef.LastName = "Ivano";
            chef.StartDate = DateTime.Now;
            chef.Certified = true;
            chef.Id = 2;
            chefs.Add(chef);

            chef = new ChefModel();
            chef.FirstName = "Lucian";
            chef.LastName = "Luciano";
            chef.StartDate = DateTime.Now;
            chef.Certified = true;
            chef.Id = 3;
            chefs.Add(chef);
        }

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

        // GET home/chef/5
        [Route("home/chef/{id}")]
        public ChefModel Get(int id)
        {
            return chefs.FirstOrDefault(c => c.Id == id);
        }

        // POST home/chef
        public void Post([FromBody] ChefModel chef)
        {
            if (chef != null)
            {
                chef.Id = chefs.Count + 1;
                chefs.Add(chef);
            }
        }

        // PUT home/chef/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE home/chef/5
        [HttpDelete]
        [Route("home/chef/{id}")]
        public void Delete(int id)
        {
            chefs.RemoveAll(c => c.Id == id);
        }


    }
}
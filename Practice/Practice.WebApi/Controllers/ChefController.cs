using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Razor.Generator;



namespace Practice.WebApi.Controllers
{
    public class ChefController : ApiController
    {

        private List<Chef> chefs;

        public ChefController()
        {
            chefs = new List<Chef>();

            Chef chef = new Chef();
            chef.FirstName = "Marko";
            chef.LastName = "Marulic";
            chef.StartDate = DateTime.Now;
            chef.Certified = false;
            chef.Id = 1;
            chefs.Add(chef);

            chef = new Chef();
            chef.FirstName = "Ivan";
            chef.LastName = "Ivano";
            chef.StartDate = DateTime.Now;
            chef.Certified = true;
            chef.Id = 2;
            chefs.Add(chef);

            chef = new Chef();
            chef.FirstName = "Lucian";
            chef.LastName = "Luciano";
            chef.StartDate = DateTime.Now;
            chef.Certified = true;
            chef.Id = 3;
            chefs.Add(chef);
        }

        // GET home/chef
        public List<Chef> Get()
        {
            return chefs;
        }

        // GET home/chef/5
        [Route("home/chef/{id}")]
        public Chef Get(int id)
        {
            return chefs.FirstOrDefault(c => c.Id == id);
        }

        // POST home/chef
        public void Post([FromBody] string value)
        {
        }

        // PUT home/chef/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE home/chef/5
        public void Delete(int id)
        {
        }


    }
}
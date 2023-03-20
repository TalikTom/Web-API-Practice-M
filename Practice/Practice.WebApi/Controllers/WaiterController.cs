using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Practice.WebApi.Models;

namespace Practice.WebApi.Controllers
{
    public class WaiterController : ApiController
    {
        List<WaiterModel> waiters = new List<WaiterModel>()
        {
               new WaiterModel { FirstName = "Arsen", LastName = "Dedic", StartDate = DateTime.Now, Id = 1, Certified = false},
               new WaiterModel { FirstName = "Kico", LastName = "Slabinac", StartDate = DateTime.Now, Id = 2, Certified = true},
               new WaiterModel { FirstName = "Ibrica", LastName = "Jusic", StartDate = DateTime.Now, Id = 3, Certified = false},
               new WaiterModel { FirstName = "Toma", LastName = "Zdravkovic", StartDate = DateTime.Now, Id = 4, Certified = true},
        };

        // GET home/waiter
        public List<WaiterModel> Get()
        {
            return waiters;
        }

        // GET home/waiter/5
        public string Get(int id)
        {
            return "value";
        }

        // POST home/waiter
        public void Post([FromBody] string value)
        {
        }

        // PUT home/waiter/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE home/waiter/5
        public void Delete(int id)
        {
        }


    }
}
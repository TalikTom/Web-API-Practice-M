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
        public static List<WaiterModel> waiters = new List<WaiterModel>()
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
        public WaiterModel Get(int id)
        {
            return waiters.FirstOrDefault(c => c.Id == id);
        }

        // POST home/waiter

        public List<WaiterModel> Post([FromBody] WaiterModel waiter)
        {
         
            waiter.Id = waiters.Count + 1;
            waiter.StartDate = DateTime.Now;
            waiters.Add(waiter);

            return Get();
           

        }

        // PUT home/waiter/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE home/waiter/5
        public List<WaiterModel> Delete(int id)
        {
            WaiterModel waiterToRemove = waiters.FirstOrDefault(c => c.Id == id);
           
            waiters.Remove(waiterToRemove);

            return Get();
        }


    }
}
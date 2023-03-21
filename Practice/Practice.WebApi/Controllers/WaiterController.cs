using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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
        public HttpResponseMessage Get()
        {
            try
            {

                if (waiters.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, waiters);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Waiters Not Found");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while executing Get all chefs method. {ex.Message}");
            }
        }

        // GET home/waiter/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                WaiterModel singleWaiter = waiters.FirstOrDefault(c => c.Id == id);

                if (singleWaiter != null)
                {
                    return Request.CreateResponse<WaiterModel>(HttpStatusCode.OK, singleWaiter);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Waiter Not Found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occured while executing Get waiter by id. {ex.Message}");
            }
        }

        // POST home/waiter

        public HttpResponseMessage Post([FromBody] WaiterModel waiter)
        {

            try
            {
                if (waiters.Any(c => c.Id == waiter.Id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A waiter with the same ID already exists.");
                }

                waiter.Id = waiters.Count + 1;
                waiter.StartDate = DateTime.Now;
                waiters.Add(waiter);

                return Request.CreateResponse<WaiterModel>(HttpStatusCode.OK, waiter);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while creating a new waiter via POST. {ex.Message}");
            }


        }

        // PUT home/waiter/5
        public HttpResponseMessage Put(int id, [FromBody] WaiterModel waiter)
        {
            try
            {
                WaiterModel waiterToUpdate = waiters.FirstOrDefault(c => c.Id == id);
                if (waiterToUpdate == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A waiter with the requested ID doesn't exist");
                }

                waiterToUpdate.FirstName = !string.IsNullOrEmpty(waiter.FirstName) ? waiter.FirstName : waiterToUpdate.FirstName;
                waiterToUpdate.LastName = !string.IsNullOrEmpty(waiter.LastName) ? waiter.LastName : waiterToUpdate.LastName;
                waiterToUpdate.StartDate = waiter.StartDate == null ? waiter.StartDate : waiterToUpdate.StartDate;
                waiterToUpdate.Certified = waiter.Certified ? waiter.Certified : waiterToUpdate.Certified;

                return Request.CreateResponse<WaiterModel>(HttpStatusCode.OK, waiterToUpdate);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while updating a waiter via PUT.{ex.Message}");
            }

        }

        // DELETE home/waiter/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                WaiterModel waiterToRemove = waiters.FirstOrDefault(c => c.Id == id);

                if (waiterToRemove == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A waiter with the requested ID doesn't exist or has already been deleted");
                }

                waiters.Remove(waiterToRemove);

                return Request.CreateResponse<WaiterModel>(HttpStatusCode.OK, waiterToRemove);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while deleting a waiter via DELETE. {ex.Message}");
            }
        }


    }
}
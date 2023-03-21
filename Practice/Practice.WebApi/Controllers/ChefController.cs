using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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

        // GET home/chef/all
        public HttpResponseMessage Get()
        {

            try
            {
                chefs.Select(c => new ChefModel
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    StartDate = c.StartDate,
                    Certified = c.Certified,
                    Id = c.Id
                }).ToList<ChefModel>();

                if (chefs.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, chefs);

                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chefs Not Found");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occurred while executing Get all chefs method.");
            }
        }

        // GET home/waiter/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ChefModel singleChef = chefs.FirstOrDefault(c => c.Id == id);

                if (singleChef != null)
                {
                    return Request.CreateResponse<ChefModel>(HttpStatusCode.OK, singleChef);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found");
                }
            }
            catch (Exception ex)
            {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Get chef by id");
            }
        }

        // POST home/waiter
        // Use [FromUri] attribute to force Web API to post the value of complex type from the query string
        // Example of URI:
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool
        public HttpResponseMessage Post([FromUri] ChefModel chef)
        {

            try
            {
                if (chefs.Any(c => c.Id == chef.Id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A chef with the same ID already exists.");
                }

                chef.Id = chefs.Count + 1;
                chef.StartDate = DateTime.Now;
                chefs.Add(chef);

                return Request.CreateResponse<ChefModel>(HttpStatusCode.OK, chef);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occurred while creating a new chef via POST.");
            }


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
        public HttpResponseMessage Delete([FromBody] int id)
        {
            ChefModel chefToRemove = chefs.FirstOrDefault(c => c.Id == id);

            if (chefToRemove == null)
            {
                HttpResponseMessage fail = new HttpResponseMessage(HttpStatusCode.NotFound);
                fail.Content = new StringContent($"Chef can not be deleted, doesn't exist, Response Code: {(int)fail.StatusCode} {fail.StatusCode}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
               
            }

            chefs.Remove(chefToRemove);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StringContent($"Chef successfully deleted, Response Code: {(int)response.StatusCode} {response.StatusCode}");

            return response;
        }


    }
}
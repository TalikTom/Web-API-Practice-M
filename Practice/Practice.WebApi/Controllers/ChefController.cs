﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Razor.Generator;
using Microsoft.Extensions.Logging;
using Practice.WebApi.Models;
using System.ComponentModel.DataAnnotations;




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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while executing Get all chefs method. {ex.Message}");
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

               return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found");

            }
            catch (Exception ex)
            {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occured while executing Get chef by id. {ex.Message}");
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while creating a new chef via POST. {ex.Message}");
            }


        }

        // PUT home/waiter/5
        // Use [FromUri] attribute to force Web API to update the value of complex type from the query string
        // Example of URI:
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool&startDate=2022-03-21T12:00:00Z
        public HttpResponseMessage Put(int id, [FromUri] ChefModel chef)
        {
            try
            {
               

                ChefModel chefToUpdate = chefs.FirstOrDefault(c => c.Id == id);
                if (chefToUpdate == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A chef with the requested ID doesn't exist");
                }

                if (!ModelState.IsValid)
                {
                    App_Start.Logger.createTxtFSSW("hey");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    
                }

                chefToUpdate.FirstName = chef.FirstName;
                chefToUpdate.LastName = chef.LastName;
                chefToUpdate.StartDate = chef.StartDate;
                chefToUpdate.Certified = chef.Certified;

                return Request.CreateResponse<ChefModel>(HttpStatusCode.OK, chefToUpdate);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while updating a chef via PUT. {ex.Message}");
            }

        }

        // DELETE home/waiter/5
        // Use[FromBody] attribute to force Web API to delete the value of primitive type from the body
        // Example of Body:
        // 2
        public HttpResponseMessage Delete([FromBody] int id)
        {

            try
            {
                ChefModel chefToRemove = chefs.FirstOrDefault(c => c.Id == id);

                if (chefToRemove == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A chef with the requested ID doesn't exist or has already been deleted");
                }

                chefs.Remove(chefToRemove);

                return Request.CreateResponse<ChefModel>(HttpStatusCode.OK, chefToRemove);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while deleting a chef via DELETE. {ex.Message}");
            }

           
        }




    }
}
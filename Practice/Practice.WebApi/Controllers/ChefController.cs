using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Razor.Generator;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using static System.Collections.Specialized.BitVector32;
using System.Reflection;
using System.Xml.Linq;
using Practice.Model;
using Practice.Service;
using System.Threading.Tasks;
using Practice.WebApi.Models;
using Practice.Service.Common;
using Practice.Common;

namespace Practice.WebApi.Controllers
{
    public class ChefController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;

        protected IChefService ChefService;

        public ChefController(IChefService chefService)
        {
            ChefService = chefService;
        }

        // GET home/chef/all
        [HttpGet]
        [Route("home/chef/get-all/")]
        public async Task<HttpResponseMessage> GetAllAsync(int page = 1, int itemsPerPage = 5, string sortBy = "Id", string sortOrder = "asc")
        {

            try
            {
                Paging paging = new Paging
                {
                    Page = page,
                    ItemsPerPage = itemsPerPage
                };

                Sorting sorting = new Sorting
                {
                    SortBy = sortBy,
                    SortOrder = sortOrder
                };


                List<ChefModel> chefs = await ChefService.GetAllAsync(paging, sorting);

                List<ChefRestGet> mappedChefs = new List<ChefRestGet>();

                

                if (chefs == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

               

                foreach (ChefModel chef in chefs)
                {
                    ChefRestGet chefRest = new ChefRestGet();
                    chefRest.FirstName = chef.FirstName;
                    chefRest.LastName = chef.LastName;
                    chefRest.HireDate = chef.HireDate;
                    mappedChefs.Add(chefRest);

                }
                              

                return Request.CreateResponse(HttpStatusCode.OK, mappedChefs);

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }
        }

        // GET home/waiter/5
        [HttpGet]
        [Route("home/chef/get-by-id/{id}")]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {

            try
            {
                

                ChefModel chef = await ChefService.GetAsync(id);

                ChefRestGet chefRest = new ChefRestGet();

                chefRest.FirstName = chef.FirstName;
                chefRest.LastName = chef.LastName;
                chefRest.HireDate = chef.HireDate;

                if (chef == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, chefRest);

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }
        }

        //POST home/chef
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool&HireDate=2022-03-21T12:00:00Z
        [HttpPost]
        [Route("home/chef/add-chef")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] ChefRestPost chefRestPost)
        {

            try
            {
                

                ChefModel chef = new ChefModel();

                chef.FirstName = chefRestPost.FirstName;
                chef.LastName = chefRestPost.LastName;
                chef.HireDate = chefRestPost.HireDate;
                chef.PhoneNumber = chefRestPost.PhoneNumber; 
                chef.HomeAddress = chefRestPost.HomeAddress;
                chef.OIB = chefRestPost.OIB;

                chef = await ChefService.PostAsync(chef);

                
               

                if (chef != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, chefRestPost);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");

            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }


        }


        // PUT home/waiter/
        // https://localhost:44334/home/chef/2?firstname=geda&lastname=fool&startDate=2022-03-21T12:00:00Z
        [HttpPut]
        [Route("home/chef/update-chef/{id}")]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] ChefRestPost chefRestPost)
        {

            try

            {
               

                ChefModel chef = new ChefModel();

                chef.FirstName = chefRestPost.FirstName;
                chef.LastName = chefRestPost.LastName;
                chef.HireDate = chefRestPost.HireDate;
                chef.PhoneNumber = chefRestPost.PhoneNumber;
                chef.HomeAddress = chefRestPost.HomeAddress;
                chef.OIB = chefRestPost.OIB;


                bool chefCheck = await ChefService.PutAsync(id, chef);

                if (!ModelState.IsValid)
                {
                    App_Start.Logger.createTxtFSSW("Model state is not valid");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                }

                if (chefCheck == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, chefRestPost);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }

        }


        //DELETE home/delete-chef/{id}

        [HttpDelete]
        [Route("home/chef/delete-chef/{id}")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {

            try
            {
                

                bool chef = await ChefService.DeleteAsync(id);

                              
                if (chef == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }


        }

    }
}
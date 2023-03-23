﻿using System;
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

namespace Practice.WebApi.Controllers
{
    public class ChefController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;



        // GET home/chef/all
        [HttpGet]
        [Route("home/chef/get-all/")]
        public HttpResponseMessage GetAll()
        {

            try
            {
                ChefService chefService = new ChefService();

                List<ChefModel> chefs = chefService.GetAll();

                chefs = chefService.GetAll();

                if (chefs == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, chefs);

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }
        }

        // GET home/waiter/5
        [HttpGet]
        [Route("home/chef/get-by-id/{id}")]
        public HttpResponseMessage Get(Guid id)
        {

            try
            {
                ChefService chefService = new ChefService();

                ChefModel chef = chefService.Get(id);


                if (chef == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, chef);

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
        public HttpResponseMessage Post([FromBody] ChefModel chef)
        {

            try
            {
                ChefService chefService = new ChefService();
                chef = chefService.Post(chef);


                if (chef != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, chef);
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
        public HttpResponseMessage Put(Guid id, [FromBody] ChefModel chef)
        {

            try

            {
                ChefService chefService = new ChefService();

                bool chefCheck = chefService.Put(id, chef);


                if (chefCheck == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, chef);
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
        public HttpResponseMessage Delete(Guid id)
        {

            try
            {
                ChefService chefService = new ChefService();

                bool chef = chefService.Delete(id);


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
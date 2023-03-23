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

namespace Practice.WebApi.Controllers
{
    public class ChefController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;



        //public static List<ChefModel> chefs = new List<ChefModel>()
        //{
        //       new ChefModel { FirstName = "Djordje", LastName = "Balasevic", StartDate = new DateTime(2022, 1, 1, 10, 2, 0), Id = 1, Certified = false},
        //       new ChefModel { FirstName = "Ciro", LastName = "Gasparac", StartDate = new DateTime(2022, 1, 1, 15, 45, 0), Id = 2, Certified = true},
        //       new ChefModel { FirstName = "Maksim", LastName = "Mrvica", StartDate = new DateTime(2022, 1, 5, 1, 0, 0), Id = 3, Certified = false},
        //       new ChefModel { FirstName = "Himzo", LastName = "Polovina", StartDate = new DateTime(2021, 12, 1, 10, 0, 30), Id = 4, Certified = true},
        //};

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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cm = new SqlCommand("INSERT INTO chef (Id, FirstName, LastName, PhoneNumber, HomeAddress, Certified, OIB, HireDate) " +
                     "VALUES (@Id, @FirstName, @LastName, @PhoneNumber, @HomeAddress, @Certified, @OIB, @HireDate)", connection);

                    chef.Id = Guid.NewGuid();

                    cm.Parameters.AddWithValue("@Id", chef.Id);
                    cm.Parameters.AddWithValue("@FirstName", chef.FirstName);
                    cm.Parameters.AddWithValue("@LastName", chef.LastName);
                    cm.Parameters.AddWithValue("@PhoneNumber", chef.PhoneNumber);
                    cm.Parameters.AddWithValue("@HomeAddress", chef.HomeAddress);
                    cm.Parameters.AddWithValue("@Certified", chef.Certified);
                    cm.Parameters.AddWithValue("@OIB", chef.OIB);
                    cm.Parameters.AddWithValue("@HireDate", chef.HireDate);

                    connection.Open();
                    int rowsAffected = cm.ExecuteNonQuery();
                    connection.Close();


                    
                    // Executing the SQL query  
                   

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, chef);
                    }

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");
                }
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
            
                if (!Helper.Checker.CheckId(id))


            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");
            }
              
                if (!ModelState.IsValid)
                {
                    App_Start.Logger.createTxtFSSW("Model state is not valid");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                }
       
          
          try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cm = new SqlCommand("update chef set Id= @id, FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, HomeAddress = @HomeAddress, Certified=@Certified, OIB=@OIB, HireDate=@HireDate where id = @id", connection);
                   
                    cm.Parameters.AddWithValue("@Id", id);
                    cm.Parameters.AddWithValue("@FirstName", chef.FirstName);
                    cm.Parameters.AddWithValue("@LastName", chef.LastName);
                    cm.Parameters.AddWithValue("@PhoneNumber", chef.PhoneNumber);
                    cm.Parameters.AddWithValue("@HomeAddress", chef.HomeAddress);
                    cm.Parameters.AddWithValue("@Certified", chef.Certified);
                    cm.Parameters.AddWithValue("@OIB", chef.OIB);
                    cm.Parameters.AddWithValue("@HireDate", chef.HireDate);

                    connection.Open();
                    int rowsAffected = cm.ExecuteNonQuery();
                    connection.Close();



                    // Executing the SQL query  


                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, chef);
                    }

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }

        }


        //DELETE home/delete-chef/{id}

        [HttpDelete]
        [Route("home/chef/delete-chef/{id}")]
        public HttpResponseMessage Delete([FromUri] Guid id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cm = new SqlCommand("Delete from chef where Id = @id", connection);


                    cm.Parameters.AddWithValue("@Id", id);


                    connection.Open();
                    int rowsAffected = cm.ExecuteNonQuery();
                    connection.Close();



                    // Executing the SQL query  


                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, id);
                    }

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");
            }


        }

    }
}
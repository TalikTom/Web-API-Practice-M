using System;
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
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

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
        public HttpResponseMessage Get()
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Creating SqlCommand object   
                    SqlCommand cm = new SqlCommand("select * from chef", connection);

                    List<ChefModel> chefs = new List<ChefModel>();
                    // Opening Connection  
                    connection.Open();
                    // Executing the SQL query  
                    SqlDataReader reader = cm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ChefModel chef = new ChefModel();

                            chef.Id = (Guid)reader["Id"];
                            chef.FirstName = (string)reader["FirstName"];
                            chef.LastName = (string)reader["LastName"];
                            chef.PhoneNumber = (string)reader["PhoneNumber"];

                            chef.HomeAddress = (string)reader["HomeAddress"];
                            chef.Certified = (bool)reader["Certified"];
                            chef.OIB = (string)reader["OIB"];
                            chef.HireDate = (DateTime)reader["HireDate"];


                            chefs.Add(chef);
                        }
                    }

                    reader.Close(); // close the SqlDataReader object

                    if (chefs.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, chefs);
                    }
                    
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");
                    
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   
                    SqlCommand cm = new SqlCommand("select * from chef where Id=@id", connection);

                    cm.Parameters.AddWithValue("@id", id);

                    connection.Open();
                   
                    SqlDataReader reader = cm.ExecuteReader();

                    ChefModel chef = new ChefModel();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            chef.Id = (Guid)reader["Id"];
                            chef.FirstName = (string)reader["FirstName"];
                            chef.LastName = (string)reader["LastName"];
                            chef.PhoneNumber = (string)reader["PhoneNumber"];
                            chef.HomeAddress = (string)reader["HomeAddress"];
                            chef.Certified = (bool)reader["Certified"];
                            chef.OIB = (string)reader["OIB"];
                            chef.HireDate = (DateTime)reader["HireDate"];
                        }
                    }

                    reader.Close();

                    if (chef != null)
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


                    cm.Parameters.AddWithValue("@Id", Guid.NewGuid());
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

        //// PUT home/waiter/5
        //// Use [FromUri] attribute to force Web API to update the value of complex type from the query string
        //// Example of URI:
        //// https://localhost:44334/home/chef/2?firstname=geda&lastname=fool&startDate=2022-03-21T12:00:00Z
        //public HttpResponseMessage Put(int id, [FromUri] ChefModel chef)
        //{
        //    try
        //    {


        //        ChefModel chefToUpdate = chefs.FirstOrDefault(c => c.Id == id);
        //        if (chefToUpdate == null)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A chef with the requested ID doesn't exist");
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            App_Start.Logger.createTxtFSSW("Model state is not valid");
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

        //        }

        //        chefToUpdate.FirstName = chef.FirstName;
        //        chefToUpdate.LastName = chef.LastName;
        //        chefToUpdate.StartDate = chef.StartDate;
        //        chefToUpdate.Certified = chef.Certified;

        //        return Request.CreateResponse<ChefModel>(HttpStatusCode.OK, chefToUpdate);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error occurred while updating a chef via PUT. {ex.Message}");
        //    }

        //}

        // DELETE home/waiter/5
        // Use[FromBody] attribute to force Web API to delete the value of primitive type from the body
        // Example of Body:
        // 2
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
using Practice.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Practice.Model;
using System.Configuration;

namespace Practice.Repository
{
    public class ChefRepository : IChefRepository
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
        public List<ChefModel> GetAll()
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

                        return chefs;
                    }


                    reader.Close(); // close the SqlDataReader object

                    if (chefs.Count > 0)
                    {
                        return null; /*Request.CreateResponse(HttpStatusCode.OK, chefs);*/
                    }

                    return null; /*Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found")*/;

                }
            }
            catch (Exception e)
            {
                return null;/*Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");*/
            }
        }

        [HttpGet]
        [Route("home/chef/get-by-id/{id}")]
        public ChefModel Get(Guid id)
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

                        return chef;
                    }

                    reader.Close();

                    if (chef != null)
                    {
                        return null; /*Request.CreateResponse(HttpStatusCode.OK, chef);*/
                    }

                    return null; /*Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chef Not Found");*/
                }

            }
            catch (Exception e)
            {
                return null;/*Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Something went wrong while processing your request. {e.Message}");*/
            }
        }

    }
}

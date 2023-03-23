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


        public List<ChefModel> GetAll()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cm = new SqlCommand("select * from chef", connection);

                    List<ChefModel> chefs = new List<ChefModel>();

                    connection.Open();

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


                    reader.Close();

                    if (chefs.Count > 0)
                    {
                        return null;
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }          

        }

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
                        return null;
                    }

                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Delete(Guid id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cm = new SqlCommand("Delete from chef where Id = @id", connection);


                cm.Parameters.AddWithValue("@Id", id);


                connection.Open();
                int rowsAffected = cm.ExecuteNonQuery();
                connection.Close();



                if (rowsAffected > 0)
                {
                    return true;
                }

                return false;
            }

        }
    }
}

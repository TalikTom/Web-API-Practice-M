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
using System.Runtime.Remoting.Messaging;

namespace Practice.Repository
{
    public class ChefRepository : IChefRepository
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;


        public List<ChefModel> GetAll()
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

        public ChefModel Get(Guid id)
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


        public ChefModel Post(ChefModel chef)
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


                if (rowsAffected > 0)
                {
                    return chef;
                }

                return null;
            }


        }

        public bool Put(Guid id, ChefModel chef)
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


                if (rowsAffected > 0)
                {
                    return true;
                }

                return false;
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

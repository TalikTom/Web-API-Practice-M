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
using Practice.Common;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Practice.Repository
{
    public class ChefRepository : IChefRepository
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;


        public async Task<List<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                StringBuilder queryBuilder = new StringBuilder("SELECT * FROM chef WHERE 1=1");


                SqlCommand cm = new SqlCommand();

                //if (!string.IsNullOrEmpty(filteringChef.FirstName))
                //{
                //    queryBuilder.Append(" AND FirstName LIKE @FirstName");
                //    cm.Parameters.AddWithValue("@FirstName", $"%{filteringChef.FirstName}%");
                //}

                //if (!string.IsNullOrEmpty(filteringChef.LastName))
                //{
                //    queryBuilder.Append(" AND LastName LIKE @LastName");
                //    cm.Parameters.AddWithValue("@LastName", $"%{filteringChef.LastName}%");
                //}

                if (filteringChef.HireDate.HasValue)
                {
                    queryBuilder.Append(" AND HireDate = @HireDate");
                    cm.Parameters.AddWithValue("@HireDate", filteringChef.HireDate.Value);
                }

                if (sorting != null)
                {
                    queryBuilder.Append($" ORDER BY {sorting.SortBy} {sorting.SortOrder}");
                }


                if (paging != null)
                {
                    //int offset = (paging.Page - 1) * paging.ItemsPerPage;
                    //int fetchNext = paging.ItemsPerPage;

                    queryBuilder.Append(" OFFSET @Offset ROWS FETCH NEXT @FetchNext ROWS ONLY");

                    cm.Parameters.AddWithValue("@Offset", (paging.Page - 1) * paging.ItemsPerPage);
                    cm.Parameters.AddWithValue("@FetchNext", paging.ItemsPerPage);
                }


                cm.CommandText = queryBuilder.ToString();

                cm.Connection = connection;

                List<ChefModelDTO> chefs = new List<ChefModelDTO>();

                connection.Open();

                SqlDataReader reader = await cm.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        ChefModelDTO chef = new ChefModelDTO();

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

        public async Task<ChefModelDTO> GetByIdAsync(Guid id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cm = new SqlCommand("select * from chef where Id=@id", connection);

                cm.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await cm.ExecuteReaderAsync();

                ChefModelDTO chef = new ChefModelDTO();

                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
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


        public async Task<ChefModelDTO> PostAsync(ChefModelDTO chef)
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

                int rowsAffected = await cm.ExecuteNonQueryAsync();

                connection.Close();


                if (rowsAffected > 0)
                {
                    return chef;
                }

                return null;
            }


        }

        public async Task<bool> PutAsync(Guid id, ChefModelDTO chef)
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
                int rowsAffected = await cm.ExecuteNonQueryAsync();
                connection.Close();


                if (rowsAffected > 0)
                {
                    return true;
                }

                return false;
            }


        }

        public async Task<bool> DeleteAsync(Guid id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cm = new SqlCommand("Delete from chef where Id = @id", connection);


                cm.Parameters.AddWithValue("@Id", id);


                connection.Open();
                int rowsAffected = await cm.ExecuteNonQueryAsync();
                connection.Close();



                if (rowsAffected > 0)
                {
                    return true;
                }

                return false;
            }

        }

        public Task<int> PostRandomChefsAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}

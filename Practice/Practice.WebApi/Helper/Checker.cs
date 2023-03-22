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


namespace Practice.WebApi.Helper
{
    public static class Checker
    {
       public static bool CheckId(Guid id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cm = new SqlCommand("select * from chef where Id=@id", connection);

                    cm.Parameters.AddWithValue("@id", id);
                    

                    connection.Open();

                    SqlDataReader reader = cm.ExecuteReader();


                if (reader.HasRows)
                {
                    return true;
                }
                else
                    
                reader.Close();

                return false;
                   
                }  
       
        }

    }
}
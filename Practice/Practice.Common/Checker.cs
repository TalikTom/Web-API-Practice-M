using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;


namespace Practice.Common.Helper
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
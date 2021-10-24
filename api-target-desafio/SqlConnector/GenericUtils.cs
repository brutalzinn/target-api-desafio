using api_target_desafio.Models;
using api_target_desafio.Models.Plans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector
{
    public  static class GenericUtils
    {
 
   


    
        public static async Task<bool> TableExists(SqlConnection Connection,string ModelName)
        {
            string query = $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '@ModelName') SELECT 1 ELSE SELECT 0";
            try
            {
                await Connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return Convert.ToBoolean(reader.GetInt32(0));
                        }
                    }
                }
                await Connection.CloseAsync();

                return false;
            }
            catch (AggregateException)
            {
                await Connection.CloseAsync();

                return false;
            }

        }

    
        public static async Task<object> Count(SqlConnection Connection, string Condition, string ModelName)
        {
            string query = $"SELECT Count(*) as count FROM {ModelName} {Condition}";
        
            try
            {
                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetInt32(0);
                            }
                        }

                    }
                }
                return 0;
            }
            catch (AggregateException)
            {

                Debug.WriteLine("Error related.");
                return 0;
            }

        }

    }
}

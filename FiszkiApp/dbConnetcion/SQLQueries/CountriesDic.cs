using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using FiszkiApp.dbConnetcion;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    public class CountriesDic
    {
        public async Task<List<string>> Countries()
        {
            MySQLCreate mySQLCreate = new MySQLCreate();
            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        using(var getCoiuntries = conn.CreateCommand())
                        {
                            getCoiuntries.Transaction = transaction;
                            getCoiuntries.CommandText = "SELECT Country FROM countries";

                            using (var reader = await getCoiuntries.ExecuteReaderAsync()) 
                            {
                                List<string> result = new List<string>();
                                while (reader.Read()) 
                                {
                                    result.Add(reader.GetString(0));
                                }
                                return result;
                            }
                        }
                    }
                } 
                catch(Exception ex)
                {
                    return new List<string>();
                }
            }
            
        }
    }
}

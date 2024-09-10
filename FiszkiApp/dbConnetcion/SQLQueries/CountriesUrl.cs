using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    public class CountriesUrl
    {
        public async Task<List<(string Country, string Url)>> CountriesU()
        {
            MySQLCreate mySQLCreate = new MySQLCreate();
            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        using (var getCoiuntries = conn.CreateCommand())
                        {
                            getCoiuntries.Transaction = transaction;
                            getCoiuntries.CommandText = "SELECT Country,Url FROM countries";

                            using (var reader = await getCoiuntries.ExecuteReaderAsync())
                            {
                                List<(string Country, string Url)> result = new List<(string, string)>();
                                while (reader.Read())
                                {
                                    string country = reader.GetString(0);
                                    string url = reader.GetString(1);

                                    result.Add((country, url));
                                }
                                return result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new List<(string, string)>();
                }
            }

        }
    }
}

using Azure.Identity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    class ProfiileDetails
    {
        public async Task<(byte[] UserImg, string UserName, string UserCountry)> UserDetails(int UserId)
        {
            MySQLCreate mySQLCreate = new MySQLCreate();
            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                byte[] userImg;
                string firstName = "";
                string lastName = "";
                string country = "";
                try
                {
                    await conn.OpenAsync();
                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        using (var getUserDetails = conn.CreateCommand())
                        {
                            getUserDetails.Transaction = transaction;
                            getUserDetails.CommandText = "SELECT FirstName, LastName, Country, Avatar From userdetails where ID_User = @userId;";
                            getUserDetails.Parameters.AddWithValue("@userId", UserId);

                            using (var reader = await getUserDetails.ExecuteReaderAsync())
                            {
                                if (reader.Read())
                                {                                   
                                    userImg = (byte[])reader["Avatar"];
                                    firstName = (string)reader["FirstName"];
                                    lastName = (string)reader["LastName"];
                                    country = (string)reader["Country"];

                                    string userName = $"{firstName} {lastName}";
                                    return (userImg, userName, country);
                                }
                                else
                                {
                                    return (null, null, null);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return (null,null,null);
                }
            }

        }   
    }
}

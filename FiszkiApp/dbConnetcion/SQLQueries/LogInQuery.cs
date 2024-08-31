using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using FiszkiApp.dbConnetcion;
using System.Transactions;
using CommunityToolkit.Maui.Converters;
using System.Runtime.Intrinsics.Arm;
using FiszkiApp.EntityClasses;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    internal class LogInQuery
    {
        byte[] PasswordToDecrypt = null;
        byte[] EncryptionKey = null;
        byte[] IV = null;
        public async Task<string> UserLogIn(string name, string password)
        {
            MySQLCreate mySQLCreate = new MySQLCreate();
            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        int ID_user;

                        using (var checkNickNameCommand = conn.CreateCommand())
                        {
                            checkNickNameCommand.Transaction = transaction;
                            checkNickNameCommand.CommandText = "SELECT ID_User FROM user WHERE UserName = @name Limit 1;";
                            checkNickNameCommand.Parameters.AddWithValue("name", name);

                            using (var reader = await checkNickNameCommand.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows)
                                {
                                    return "Hasło lub login jest nie poprawne";
                                }
                                else
                                {
                                    reader.Read();
                                     ID_user = (int)reader["ID_User"];
                                }
                            }
                        }

                        using (var getPasswordFromUserId = conn.CreateCommand())
                        {
                            getPasswordFromUserId.Transaction = transaction;
                            getPasswordFromUserId.CommandText = "SELECT UserPassword FROM user Where ID_User = @ID_user";
                            getPasswordFromUserId.Parameters.AddWithValue($"ID_User", ID_user);

                            using (var readerPass = await getPasswordFromUserId.ExecuteReaderAsync())
                            {
                                if (!readerPass.HasRows)
                                {
                                    return "Hasło lub login jest nie poprawne";
                                }
                                else
                                {
                                    readerPass.Read();
                                    PasswordToDecrypt = (byte[])readerPass["UserPassword"];

                                }
                            }
                        }
                        using (var getEncryptionKeys = conn.CreateCommand())
                        {
                            getEncryptionKeys.Transaction = transaction;
                            getEncryptionKeys.CommandText = "SELECT EncryptionKey, IV FROM EncryptionKeys Where ID_User = @ID_user";
                            getEncryptionKeys.Parameters.AddWithValue($"ID_User", ID_user);

                            using (var readerKey = await getEncryptionKeys.ExecuteReaderAsync())
                            {
                                if (!readerKey.HasRows)
                                {
                                    return "Hasło lub login jest nie poprawne";
                                }
                                else
                                {
                                    readerKey.Read();
                                    EncryptionKey = (byte[])readerKey["EncryptionKey"];
                                    IV = (byte[])readerKey["IV"];
                                    string decryptedPassword = EntityClasses.AesManaged.Decryption(PasswordToDecrypt, EncryptionKey, IV);
                                    Console.WriteLine($"Decrypted Password: {decryptedPassword}");
                                    if (decryptedPassword != null && decryptedPassword == password)
                                    {
                                        //User login Sacessfull 
                                        return Convert.ToString(ID_user);
                                    }
                                    else
                                    {
                                        return "Hasło lub login jest nie poprawne";
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "Wystąpił błąd podczas logowania";
                }

            }
        }
    }
}

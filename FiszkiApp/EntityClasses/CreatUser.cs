using FiszkiApp.dbConnetcion;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.EntityClasses
{
    internal class CreatUser
    {
        public async Task<string> UserInsertAsync(string Name, byte[] Password, string FirstName, string Lastname, string Country, string Email, byte[] Image, bool IsAcceptedPolicy, byte[] IV, byte[] EncryptionKey)
        {
            MySQLCreate mySQLCreate = new MySQLCreate();
            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        using (var checkEmailCommand = conn.CreateCommand())
                        {
                            checkEmailCommand.Transaction = transaction;
                            checkEmailCommand.CommandText = "SELECT COUNT(ID_User) FROM userDetails WHERE Email = @Email LIMIT 1;";
                            checkEmailCommand.Parameters.AddWithValue("@Email", Email);

                            using (var reader = await checkEmailCommand.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync() && reader.GetInt32(0) > 0)
                                {
                                    return "Istnieje już użytkownik o podanym emailu";
                                }
                            }
                        }

                        using (var checkNameCommand = conn.CreateCommand())
                        {
                            checkNameCommand.Transaction = transaction;
                            checkNameCommand.CommandText = "SELECT COUNT(ID_User) FROM user WHERE UserName = @Name LIMIT 1;";
                            checkNameCommand.Parameters.AddWithValue("@Name", Name);

                            using (var reader = await checkNameCommand.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync() && reader.GetInt32(0) > 0)
                                {
                                    return "Istnieje już użytkownik o podanej nazwie";
                                }
                            }
                        }

                        using (var insertUserCommand = conn.CreateCommand())
                        {
                            insertUserCommand.Transaction = transaction;
                            insertUserCommand.CommandText = "INSERT INTO user (UserName, UserPassword) VALUES(@Name, @Password);";
                            insertUserCommand.Parameters.AddWithValue("@Name", Name);
                            insertUserCommand.Parameters.AddWithValue("@Password", Password);

                            await insertUserCommand.ExecuteNonQueryAsync();
                            int userId = (int)insertUserCommand.LastInsertedId;

                            using (var insertDetailsCommand = conn.CreateCommand())
                            {
                                insertDetailsCommand.Transaction = transaction;
                                insertDetailsCommand.CommandText = @"INSERT INTO userDetails 
                                                                   (ID_User, FirstName, LastName, Country, Email, Avatar, IsAcceptedPolicy) 
                                                                   VALUES (@UserID, @FirstName, @LastName, @Country, @Email, @Image, @IsAcceptedPolicy);";

                                insertDetailsCommand.Parameters.AddWithValue("@UserID", userId);
                                insertDetailsCommand.Parameters.AddWithValue("@FirstName", FirstName);
                                insertDetailsCommand.Parameters.AddWithValue("@LastName", Lastname);
                                insertDetailsCommand.Parameters.AddWithValue("@Country", Country);
                                insertDetailsCommand.Parameters.AddWithValue("@Email", Email);
                                insertDetailsCommand.Parameters.AddWithValue("@Image", Image);
                                insertDetailsCommand.Parameters.AddWithValue("@IsAcceptedPolicy", IsAcceptedPolicy);

                                await insertDetailsCommand.ExecuteNonQueryAsync();
                            }
                            using (var insertKeyCommand = conn.CreateCommand())
                            {
                                insertKeyCommand.Transaction = transaction;
                                insertKeyCommand.CommandText = @"INSERT INTO EncryptionKeys 
                                                                   (ID_User, EncryptionKey, IV)
                                                                   VALUES (@UserID, @EncryptionKey, @IV);";

                                insertKeyCommand.Parameters.AddWithValue("@UserID", userId);
                                insertKeyCommand.Parameters.AddWithValue("@EncryptionKey", EncryptionKey);
                                insertKeyCommand.Parameters.AddWithValue("@IV", IV);


                                await insertKeyCommand.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();

                        return "Rejstracja zakończyła się sukcesem";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return "wystąpił błąd podczas rejestracji";
                }

            }
        }
    }
}
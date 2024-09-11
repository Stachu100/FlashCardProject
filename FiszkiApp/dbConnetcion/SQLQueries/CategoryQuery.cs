using FiszkiApp.Models;
using MySqlConnector;
using System;
using System.Threading.Tasks;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    internal class CategoryQuery
    {
        public async Task<string> AddCategoryAsync(int userId, string CategoryName, string FrontLanguage, string BackLanguage, string LanguageLevel)
        {
            MySQLCreate mySQLCreate = new MySQLCreate();
            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        using (var insertCategoryCommand = conn.CreateCommand())
                        {
                            insertCategoryCommand.Transaction = transaction;
                            insertCategoryCommand.CommandText = @"
                                INSERT INTO Category (UserID, CategoryName, FrontLanguage, BackLanguage, LanguageLevel)
                                VALUES (@UserID, @CategoryName, @FrontLanguage, @BackLanguage, @LanguageLevel);
                            ";

                            insertCategoryCommand.Parameters.AddWithValue("@UserID", userId);
                            insertCategoryCommand.Parameters.AddWithValue("@CategoryName", CategoryName);
                            insertCategoryCommand.Parameters.AddWithValue("@FrontLanguage", FrontLanguage);
                            insertCategoryCommand.Parameters.AddWithValue("@BackLanguage", BackLanguage);
                            insertCategoryCommand.Parameters.AddWithValue("@LanguageLevel", LanguageLevel ?? (object)DBNull.Value);

                            await insertCategoryCommand.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                        return "Kategoria została dodana pomyślnie";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                    return "Wystąpił błąd podczas dodawania kategorii";
                }
            }
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync(int userId)
        {
            var categories = new List<Category>();

            using (var conn = new MySqlConnection(MySQLCreate.connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ID_Category, CategoryName, FrontLanguage, BackLanguage FROM Category WHERE UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetInt32("ID_Category"),
                                CategoryName = reader.GetString("CategoryName"),
                                FrontLanguage = reader.GetString("FrontLanguage"),
                                BackLanguage = reader.GetString("BackLanguage"),
                            });
                        }
                    }
                }
            }

            return categories;
        }
    }
}

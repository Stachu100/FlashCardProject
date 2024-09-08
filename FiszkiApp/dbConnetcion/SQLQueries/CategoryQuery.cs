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
    }
}

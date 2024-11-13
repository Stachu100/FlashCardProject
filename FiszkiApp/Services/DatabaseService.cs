using SQLite;
using System.IO;
using System.Threading.Tasks;
using FiszkiApp.EntityClasses.Models;

namespace FiszkiApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<LocalCategoryTable>().Wait();
        }

        public Task<int> AddCategoryAsync(LocalCategoryTable category)
        {
            return _database.InsertAsync(category);
        }

        public Task<int> UpdateCategoryAsync(LocalCategoryTable category)
        {
            return _database.UpdateAsync(category);
        }

        public Task<int> DeleteCategoryAsync(LocalCategoryTable category)
        {
            return _database.DeleteAsync(category);
        }

        public Task<List<LocalCategoryTable>> GetCategoriesAsync()
        {
            return _database.Table<LocalCategoryTable>().ToListAsync();
        }

        public Task<LocalCategoryTable> GetCategoryByIdAsync(int id)
        {
            return _database.Table<LocalCategoryTable>().Where(i => i.IdCategory == id).FirstOrDefaultAsync();
        }
    }
}

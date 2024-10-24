using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MySqlConnector;

namespace FiszkiApp.dbConnetcion
{
     class MySQLCreate
    {
        public static string connectionString;

        public MySQLCreate()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysql4.webio.pl",
                Database = "23512_flashcard_db",
                UserID = "23512_admincard",
                Password = "3fzpm(hcyqabjlweunso",
                SslMode = MySqlSslMode.Required,
            };
            connectionString = builder.ConnectionString;            
        }
    }
}

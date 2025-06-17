using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Data
{
    internal class DBManager
    {
        public static string GetConnectionString()
        {
            Console.WriteLine($"Connection string (without password): {Properties.Settings.Default.DBUsername}");
            return $"Data Source={Properties.Settings.Default.DBServer};" +
                   $"Initial Catalog={Properties.Settings.Default.DBName};" +
                   $"User ID={Properties.Settings.Default.DBUsername};" +
                   $"Password={Properties.Settings.Default.DBPassword};";
        }

        public static bool TestConnection()
        {
            try
            {
                // using used to prevent memory leaks or i have to used connection.Dispose
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    return true;
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Connection error: {e.Message}");
                return false;
            }
        }
    }
}

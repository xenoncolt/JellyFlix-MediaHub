using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JellyFlix_MediaHub.Utils;
using JellyFlix_MediaHub.Models;
using System.Data;
using System.Data.SqlClient;

namespace JellyFlix_MediaHub.Data.Handlers
{
    internal class UserHandle
    {
        public static bool RegisterUser(string username, string email, string password, string role)
        {
            try
            {
                Console.WriteLine($"Attempting to register user: {username}, {email}");
                string salt_pass = PasswordSecure.GenerateSaltPass();
                string hashed_pass = PasswordSecure.HashPassword(password, salt_pass);
                Console.WriteLine("Password hashed successfully");

                var parameters = new Dictionary<string, object>
                {
                    { "username", username },
                    { "email", email },
                    { "password", hashed_pass },
                    { "salt", salt_pass },
                    { "role", role },
                    { "created_date", DateTime.Now }
                };

                Console.WriteLine("Executing INSERT operation...");
                int result = (int)DBOperations.ExecuteOperation(DatabaseOperation.INSERT, "Users", parameters);
                Console.WriteLine($"INSERT operation result: {result}");

                return result > 0;
            } catch (SqlException e)
            {
                Console.WriteLine($"SQL Error Number: {e.Number}");
                Console.WriteLine($"SQL Error: {e.Message}");
                return false;
            } catch (Exception e)
            {
                Console.WriteLine($"Error while registering user: {e.GetType().Name}: {e.Message}");
                Console.WriteLine($"Stack trace: {e.StackTrace}");
                return false;
            }
        }

        public static User AuthenticateUser(string username, string password)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "username", username }
                };

                object query_result = DBOperations.ExecuteOperation(DatabaseOperation.SELECT, "Users", parameters, "username = @username");

                if (query_result == null)
                {
                    Console.WriteLine("No Username and Password Found. So return NULL");
                    return null;
                }

                DataTable result = (DataTable)query_result;

                if (result.Rows.Count == 0) return null;

                string stored_hash = result.Rows[0]["password"].ToString();
                string salt = result.Rows[0]["salt"].ToString();

                if (PasswordSecure.VerifyPassword(password, salt, stored_hash))
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(result.Rows[0]["user_id"]),
                        Username = result.Rows[0]["username"].ToString(),
                        Email = result.Rows[0]["email"].ToString(),
                        Role = result.Rows[0]["role"].ToString(),
                        CreatedDate = Convert.ToDateTime(result.Rows[0]["created_date"])
                    };
                }

                return null;
            } catch (Exception e)
            {
                Console.WriteLine($"Authentication Error: {e.Message}");
                return null;
            }
        }

        public static bool IsUsersDBEmpty()
        {
            try
            {
                object result = DBOperations.ExecuteOperation(DatabaseOperation.SELECT, "Users", null, null, "Count(*) AS UserCount");

                if (result == null) return true;

                DataTable data_table = (DataTable)result;

                if (data_table.Rows.Count > 0)
                {
                    int user_count = Convert.ToInt32(data_table.Rows[0]["UserCount"]);
                    return user_count == 0;
                }

                return true;
            } catch (Exception e)
            {
                Console.WriteLine($"Error checking Users database: {e.Message}");
                return false;
            }
        }
    }
}

﻿using JellyFlix_MediaHub.Models;
using JellyFlix_MediaHub.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace JellyFlix_MediaHub.Data.Handlers
{
    internal class UserHandle
    {
        public static bool RegisterUser(string username, string email, string password, int role_id)
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
                    { "role_id", role_id },
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

                string role;
                switch(result.Rows[0]["role_id"].ToString())
                {
                    case "1":
                        role = "admin";
                        break;
                    case "2":
                        role = "user";
                        break;
                    case "3":
                        role = "premium";
                        break;
                    default:
                        role = "user";
                        break;
                }

                if (PasswordSecure.VerifyPassword(password, salt, stored_hash))
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(result.Rows[0]["user_id"]),
                        Username = result.Rows[0]["username"].ToString(),
                        Email = result.Rows[0]["email"].ToString(),
                        Password = password,
                        Role = role,
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

        public static bool UpdateUser(int userId, string new_username = null, string new_email = null, string new_password = null)
        {
            try
            {
                var parameters = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(new_username))
                {
                    parameters.Add("username", new_username);
                }

                if (!string.IsNullOrEmpty(new_email))
                {
                    parameters.Add("email", new_email);
                }

                if (!string.IsNullOrEmpty(new_password))
                {
                    string new_salt = PasswordSecure.GenerateSaltPass();
                    string new_hashed_pass = PasswordSecure.HashPassword(new_password, new_salt);
                    parameters.Add("password", new_hashed_pass);
                    parameters.Add("salt", new_salt);
                }

                if (parameters.Count == 0)
                {
                    Console.WriteLine("No fields provided for update");
                    return false;
                }

                Console.WriteLine($"Updating user with ID: {userId}");
                int result = (int)DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "Users",
                    parameters,
                    $"user_id = {userId}");

                Console.WriteLine($"UPDATE operation result: {result}");
                return result > 0;
            } catch (SqlException e)
            {
                Console.WriteLine($"SQL Error Number: {e.Number}");
                Console.WriteLine($"SQL Error: {e.Message}");
                return false;
            } catch (Exception e)
            {
                Console.WriteLine($"Error while updating user: {e.GetType().Name}: {e.Message}");
                Console.WriteLine($"Stack trace: {e.StackTrace}");
                return false;
            }
        }

        public static List<User> GetAllUsers()
        {
            try
            {
                List<User> users = new List<User>();

                object query_result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "Users",
                    null,
                    null,
                    "user_id, username, email, role_id, created_date");

                if (query_result == null)
                {
                    Console.WriteLine("No users found in DB");
                    return users;
                }

                DataTable result = (DataTable)query_result;

                foreach (DataRow row in result.Rows)
                {
                    string role;
                    switch (row["role_id"].ToString())
                    {
                        case "1":
                            role = "admin";
                            break;
                        case "2":
                            role = "user";
                            break;
                        case "3":
                            role = "premium";
                            break;
                        default:
                            role = "user";
                            break;
                    }

                    users.Add(new User
                    {
                        UserId = Convert.ToInt32(row["user_id"]),
                        Username = row["username"].ToString(),
                        Email = row["email"].ToString(),
                        Role = role,
                        CreatedDate = Convert.ToDateTime(row["created_date"])
                    });
                }
                return users;
            } catch (SqlException e)
            {
                Console.WriteLine($"Error fetch all users: {e.Message}");
                return new List<User>();
            }
        }

        public static bool UpdateUserRole(int userId, string new_role)
        {
            try
            {
                int role_id;
                switch (new_role.ToLower())
                {
                    case "admin":
                        role_id = 1;
                        break;
                    case "premium":
                        role_id = 3;
                        break;
                    case "user":
                    default:
                        role_id = 2;
                        break;
                }

                var parameters = new Dictionary<string, object>
                {
                    { "role_id", role_id }
                };

                Console.WriteLine($"Updating user role for user ID: {userId} to {new_role}");
                int result = (int)DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "Users",
                    parameters,
                    $"user_id = {userId}");
                Console.WriteLine($"UPDATE operation result: {result}");
                return result > 0;
            } catch (Exception e)
            {
                Console.WriteLine($"Error while Update User role : {e.Message}");
                return false;
            }
        }

        public static bool DeleteUser(int userId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "user_id", userId }
                };

                Console.WriteLine($"Deleting user with ID: {userId}");
                int result = (int)DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "Users",
                    parameters,
                    "user_id = @user_id");

                Console.WriteLine($"DELETE operation result: {result}");
                return result > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error Number: {e.Number}");
                Console.WriteLine($"SQL Error: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while deleting user: {e.GetType().Name}: {e.Message}");
                Console.WriteLine($"Stack trace: {e.StackTrace}");
                return false;
            }
        }
    }
}

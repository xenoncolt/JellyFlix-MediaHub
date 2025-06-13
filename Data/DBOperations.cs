using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Data
{
    public enum DatabaseOperation
    {
        SELECT,
        INSERT,
        UPDATE,
        DELETE
    }
    internal class DBOperations
    {
        public static DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            if (DBManager.TestConnection())
            {
                using (SqlConnection connection = new SqlConnection(DBManager.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    DataTable result = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    try
                    {
                        connection.Open();
                        adapter.Fill(result);
                        return result;
                    } catch (Exception e)
                    {
                        Console.WriteLine($"Query error: {e.Message}");
                        throw;
                    }
                }
            } else
            {
                return null;
            }
        }

        public static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            if (DBManager.TestConnection())
            {
                using (SqlConnection connection = new SqlConnection(DBManager.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        return cmd.ExecuteNonQuery();
                    } catch (Exception e)
                    {
                        Console.WriteLine($"Command error: {e.Message}");
                        throw;
                    }
                }
            } else
            {
                return default;
            }
        }

        public static object ExecuteOperation(DatabaseOperation operation, string table_name, Dictionary<string, object> parameters, string where_clause = null, string select_columns = "*")
        {
            string sql;

            switch(operation)
            {
                case DatabaseOperation.SELECT:
                    sql = $"SELECT {select_columns} FROM {table_name}";
                    if (!string.IsNullOrEmpty(where_clause))
                    {
                        sql += $"WHERE {where_clause}";
                    }
                    return ExecuteQuery(sql, parameters);

                case DatabaseOperation.INSERT:
                    if (parameters == null || parameters.Count == 0)
                    {
                        throw new ArgumentException("Parameters are required for INSERT operation");
                    }

                    string columns = string.Join(", ", parameters.Keys);
                    string values = string.Join(", ", parameters.Keys.Select(key => "@" + key));

                    sql = $"INSERT INTO {table_name} ({columns}) VALUES ({values})";
                    return ExecuteNonQuery(sql, parameters);

                case DatabaseOperation.UPDATE:
                    if (parameters == null || parameters.Count == 0)
                    {
                        throw new ArgumentException("Forget a parameters to use UPDATE operation");
                    }

                    if (string.IsNullOrEmpty(where_clause)) throw new ArgumentException("WHERE clauses needed bro");

                    string set_clause = string.Join(", ", parameters.Keys.Select(key => $"{key} = @{key}"));

                    sql = $"UPDATE {table_name} SET {set_clause} WHERE {where_clause}";
                    return ExecuteNonQuery(sql, parameters);

                case DatabaseOperation.DELETE:
                    if (string.IsNullOrEmpty(where_clause))
                    {
                        throw new ArgumentException("WHERE clause need for DELETE Operation");
                    }
                    sql = $"DELETE FROM {table_name} WHERE {where_clause}";
                    return ExecuteNonQuery(sql, parameters);

                default:
                    throw new ArgumentException("U r missing something. Only [SELECT, INSERT, UPDATE, DELETE]");
            }
        }
    }
}

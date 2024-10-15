using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.entity;
using OrderManagementSystem.Util;
using OrderManagementSystem.exceptions;
using System.Data.SqlClient;

namespace OrderManagementSystem.dao
{
    public class UserDaoImpl : IUserDao
    {
        private readonly string connectionString;

        public UserDaoImpl()
        {
            // Get connection string from DBPropertyUtil
            connectionString = DBPropertyUtil.GetConnectionString("db.properties"); 
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public User GetUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User
                            {
                                UserId = (int)reader["UserId"],
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = reader["Role"].ToString()
                            };

                            return user;
                        }
                    }
                }
            }
            return null; // Return null if not found
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                UserId = (int)reader["UserId"],
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = reader["Role"].ToString()
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public List<Dictionary<string, object>> GetOrdersByUser(User user)
        {
            List<Dictionary<string, object>> orders = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT o.OrderId, o.OrderDate, o.TotalAmount FROM Orders o INNER JOIN Users u ON o.UserId = u.UserId WHERE u.UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetails = new Dictionary<string, object>
                            {
                                { "OrderId", reader["OrderId"] },
                                { "OrderDate", reader["OrderDate"] },
                                { "TotalAmount", reader["TotalAmount"] }
                            };

                            orders.Add(orderDetails);
                        }
                    }
                }
            }

            return orders;
        }
    }
}
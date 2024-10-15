using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.entity;
using OrderManagementSystem.Util;
using OrderManagementSystem.exceptions;
using System.Data.SqlClient;
using OrderManagementSystem.Util.OrderManagementSystem.Util;

namespace OrderManagementSystem.dao
{
    public class UserDaoImpl : IUserDao
    {
        public void AddUser(User user)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "INSERT INTO Users (userId, username, password, role) VALUES (@userId, @username, @password, @role)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", user.UserId);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "UPDATE Users SET username = @username, password = @password, role = @role WHERE userId = @userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", user.UserId);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "DELETE FROM Users WHERE userId = @userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User GetUserById(int userId)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "SELECT * FROM Users WHERE userId = @userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = (int)reader["userId"],
                                Username = (string)reader["username"],
                                Password = (string)reader["password"],
                                Role = (string)reader["role"]
                            };
                        }
                    }
                }
            }
            return null; // Return null if user is not found
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "SELECT * FROM Users";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = (int)reader["userId"],
                                Username = (string)reader["username"],
                                Password = (string)reader["password"],
                                Role = (string)reader["role"]
                            });
                        }
                    }
                }
            }
            return users; // Return the list of users
        }

        public List<Dictionary<string, object>> GetOrdersByUser(User user)
        {
            List<Dictionary<string, object>> orders = new List<Dictionary<string, object>>();
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "SELECT * FROM Orders WHERE userId = @userId"; // Assuming you have an Orders table
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", user.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Dictionary<string, object>
                            {
                                { "OrderId", reader["orderId"] },
                                { "ProductId", reader["productId"] },
                                { "Quantity", reader["quantity"] },
                                { "OrderDate", reader["orderDate"] }
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders; // Return the list of orders for the user
        }
    }
}
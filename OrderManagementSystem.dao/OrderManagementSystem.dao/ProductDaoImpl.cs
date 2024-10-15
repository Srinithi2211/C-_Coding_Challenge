using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OrderManagementSystem.entity;
using OrderManagementSystem.Util;
using OrderManagementSystem.dao;
using System.Configuration;

namespace OrderManagementSystem.dao
{
    public class ProductDaoImpl : IProductDao
    {
        private readonly string connectionString;

        public ProductDaoImpl()
        {
            // Get connection string from DBPropertyUtil
            connectionString = DBPropertyUtil.GetConnectionString("db.properties"); 
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (ProductName, Description, Price, QuantityInStock, Type, Brand, WarrantyPeriod) VALUES (@ProductName, @Description, @Price, @QuantityInStock, @Type, @Brand, @WarrantyPeriod)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    command.Parameters.AddWithValue("@Type", product.Type);

                    // Check if it's an Electronics or Clothing product
                    if (product is Electronics electronics)
                    {
                        command.Parameters.AddWithValue("@Brand", electronics.Brand);
                        command.Parameters.AddWithValue("@WarrantyPeriod", electronics.WarrantyPeriod);
                    }
                    else if (product is Clothing clothing)
                    {
                        command.Parameters.AddWithValue("@Brand", DBNull.Value); // No brand for Clothing
                        command.Parameters.AddWithValue("@WarrantyPeriod", DBNull.Value); // No warranty for Clothing
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price, QuantityInStock = @QuantityInStock, Type = @Type WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    command.Parameters.AddWithValue("@Type", product.Type);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Product GetProductById(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products WHERE ProductId = @ProductId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string type = reader["Type"].ToString();
                            Product product;
                            if (type == "Electronics")
                            {
                                product = new Electronics
                                {
                                    ProductId = (int)reader["ProductId"],
                                    ProductName = reader["ProductName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (double)reader["Price"],
                                    QuantityInStock = (int)reader["QuantityInStock"],
                                    Type = type,
                                    Brand = reader["Brand"].ToString(),
                                    WarrantyPeriod = (int)reader["WarrantyPeriod"]
                                };
                            }
                            else
                            {
                                product = new Clothing
                                {
                                    ProductId = (int)reader["ProductId"],
                                    ProductName = reader["ProductName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (double)reader["Price"],
                                    QuantityInStock = (int)reader["QuantityInStock"],
                                    Type = type,
                                    Size = reader["Size"].ToString(),
                                    Color = reader["Color"].ToString()
                                };
                            }

                            return product;
                        }
                    }
                }
            }
            return null; // Return null if not found
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader["Type"].ToString();
                            Product product;
                            if (type == "Electronics")
                            {
                                product = new Electronics
                                {
                                    ProductId = (int)reader["ProductId"],
                                    ProductName = reader["ProductName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (double)reader["Price"],
                                    QuantityInStock = (int)reader["QuantityInStock"],
                                    Type = type,
                                    Brand = reader["Brand"].ToString(),
                                    WarrantyPeriod = (int)reader["WarrantyPeriod"]
                                };
                            }
                            else
                            {
                                product = new Clothing
                                {
                                    ProductId = (int)reader["ProductId"],
                                    ProductName = reader["ProductName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (double)reader["Price"],
                                    QuantityInStock = (int)reader["QuantityInStock"],
                                    Type = type,
                                    Size = reader["Size"].ToString(),
                                    Color = reader["Color"].ToString()
                                };
                            }

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
}
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
using OrderManagementSystem.Util.OrderManagementSystem.Util;

namespace OrderManagementSystem.dao
{
    public class ProductDaoImpl : IProductDao

    {
        public void AddProduct(Product product)
        {
            CreateProduct(product); // Use CreateProduct to insert the product into the database
        }
        public void CreateProduct(Product product)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                
                string query = "INSERT INTO Products (productId, productName, description, price, quantityInStock, type) VALUES (@productId, @productName, @description, @price, @quantityInStock, @type)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);
                    cmd.Parameters.AddWithValue("@productName", product.ProductName);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);
                    cmd.Parameters.AddWithValue("@type", product.Type);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "UPDATE Products SET productName = @productName, description = @description, price = @price, quantityInStock = @quantityInStock, type = @type WHERE productId = @productId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);
                    cmd.Parameters.AddWithValue("@productName", product.ProductName);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);
                    cmd.Parameters.AddWithValue("@type", product.Type);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "DELETE FROM Products WHERE productId = @productId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Product GetProductById(int productId)
        {
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "SELECT * FROM Products WHERE productId = @productId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                ProductId = (int)reader["productId"],
                                ProductName = (string)reader["productName"],
                                Description = (string)reader["description"],
                                Price = (double)reader["price"],
                                QuantityInStock = (int)reader["quantityInStock"],
                                Type = (string)reader["type"]
                            };
                        }
                    }
                }
            }
            return null; // Return null if no product is found
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = DBUtil.getDBConnection())
            {
                string query = "SELECT * FROM Products";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = (int)reader["productId"],
                                ProductName = (string)reader["productName"],
                                Description = (string)reader["description"],
                                Price = (double)reader["price"],
                                QuantityInStock = (int)reader["quantityInStock"],
                                Type = (string)reader["type"]
                            });
                        }
                    }
                }
            }
            return products; // Return the list of products
        }
    }
}
using OrderManagementSystem.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.dao
{
    public interface IProductDao
    {
        void AddProduct(Product product);
        Product GetProductById(int productId);
        List<Product> GetAllProducts();
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}

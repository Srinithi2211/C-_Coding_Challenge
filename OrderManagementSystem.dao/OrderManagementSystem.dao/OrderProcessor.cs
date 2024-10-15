using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.entity;
using OrderManagementSystem.Util;
using OrderManagementSystem.exceptions;

namespace OrderManagementSystem.dao
{
    public class OrderProcessor : IOrderManagementRepository
    {
        private readonly IUserDao userDao;
        private readonly IProductDao productDao;
        // Add other necessary DAOs

        public OrderProcessor(IUserDao userDao, IProductDao productDao) // Inject DAOs via constructor
        {
            this.userDao = userDao;
            this.productDao = productDao;
        }

        public void CreateOrder(User user, List<Product> products)
        {
            // Check if user exists
            var existingUser = userDao.GetUserById(user.UserId);
            if (existingUser == null)
            {
                // If not, create the user
                CreateUser(user);
            }
            // Create order logic here (store the order details in DB)
            // Example: Order order = new Order(user, products); 
            // Save order to DB
        }

        public void CancelOrder(int userId, int orderId)
        {
            // Check if userId exists
            var existingUser = userDao.GetUserById(userId);
            if (existingUser == null)
                throw new UserNotFoundException("User not found.");

            // Check if orderId exists (you may need to implement this in the OrderDao)
            // If orderId is not found, throw an OrderNotFoundException

            // Logic to cancel the order in the database
        }

        public void CreateProduct(User user, Product product)
        {
            if (user.Role != "Admin")
                throw new UnauthorizedAccessException("Only admins can create products.");

            productDao.AddProduct(product); // Assuming AddProduct method exists in productDao
        }

        public void CreateUser(User user)
        {
            userDao.AddUser(user); // Assuming AddUser method exists in userDao
        }

        public List<Product> GetAllProducts()
        {
            return productDao.GetAllProducts(); // Assuming GetAllProducts method exists in productDao
        }

        public List<Dictionary<string, object>> GetOrdersByUser(User user)
        {
            return userDao.GetOrdersByUser(user); // You need to implement this logic in productDao
        }
    }

}

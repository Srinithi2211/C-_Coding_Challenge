using OrderManagementSystem.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderManagementSystem.dao
{
    public interface IUserDao
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUserById(int userId);
        List<User> GetAllUsers();
        List<Dictionary<string, object>> GetOrdersByUser(User user);
    }
}

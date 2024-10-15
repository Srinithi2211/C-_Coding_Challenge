using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.exceptions
{
    // Base exception class
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }

    // Exception for user not found
    public class UserNotFoundException : CustomException
    {
        public UserNotFoundException(string message) : base(message) { }
    }

    // Exception for order not found
    public class OrderNotFoundException : CustomException
    {
        public OrderNotFoundException(string message) : base(message) { }
    }

    

    // Exception for invalid operation (e.g., user already exists)
    public class InvalidOperationException : CustomException
    {
        public InvalidOperationException(string message) : base(message) { }
    }
}

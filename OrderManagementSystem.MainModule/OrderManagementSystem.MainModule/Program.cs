using OrderManagementSystem.entity;
using System;
using System.Collections.Generic;
using OrderManagementSystem.Util;
class MainModule
{
    static void Main(string[] args)
    {
        // Create instances of your DAO implementations
        IUserDao userDao = new UserDaoImpl();
        IProductDao productDao = new ProductDaoImpl();
        IOrderManagementRepository orderManagement = new OrderProcessor(); // Assuming you have this implemented

        while (true)
        {
            Console.WriteLine("----- Order Management System -----");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Create Product");
            Console.WriteLine("3. Create Order");
            Console.WriteLine("4. Cancel Order");
            Console.WriteLine("5. Get All Products");
            Console.WriteLine("6. Get Orders by User");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Create User
                    User newUser = GetUserInput();
                    userDao.CreateUser(newUser);
                    Console.WriteLine("User created successfully.");
                    break;

                case "2":
                    // Create Product
                    Product newProduct = GetProductInput();
                    productDao.CreateProduct(newProduct);
                    Console.WriteLine("Product created successfully.");
                    break;

                case "3":
                    // Create Order
                    User orderUser = GetUserInput(); // Get user info
                    List<Product> products = GetProductListInput(); // Get product list
                    orderManagement.createOrder(orderUser, products);
                    Console.WriteLine("Order created successfully.");
                    break;

                case "4":
                    // Cancel Order
                    Console.Write("Enter User ID: ");
                    int userId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Order ID: ");
                    int orderId = Convert.ToInt32(Console.ReadLine());
                    orderManagement.cancelOrder(userId, orderId);
                    Console.WriteLine("Order canceled successfully.");
                    break;

                case "5":
                    // Get All Products
                    var allProducts = productDao.GetAllProducts();
                    foreach (var product in allProducts)
                    {
                        Console.WriteLine(product);
                    }
                    break;

                case "6":
                    // Get Orders by User
                    User searchUser = GetUserInput(); // Get user info
                    var userOrders = orderManagement.getOrderByUser(searchUser);
                    foreach (var order in userOrders)
                    {
                        Console.WriteLine(order);
                    }
                    break;

                case "7":
                    // Exit
                    Console.WriteLine("Exiting the application.");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine(); // Blank line for readability
        }
    }

    private static User GetUserInput()
    {
        Console.Write("Enter User ID: ");
        int userId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();
        Console.Write("Enter Password: ");
        string password = Console.ReadLine();
        Console.Write("Enter Role (Admin/User): ");
        string role = Console.ReadLine();

        return new User { userId = userId, username = username, password = password, role = role };
    }

    private static Product GetProductInput()
    {
        Console.Write("Enter Product ID: ");
        int productId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Product Name: ");
        string productName = Console.ReadLine();
        Console.Write("Enter Description: ");
        string description = Console.ReadLine();
        Console.Write("Enter Price: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter Quantity in Stock: ");
        int quantityInStock = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Type (Electronics/Clothing): ");
        string type = Console.ReadLine();

        if (type.Equals("Electronics", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Enter Brand: ");
            string brand = Console.ReadLine();
            Console.Write("Enter Warranty Period (months): ");
            int warrantyPeriod = Convert.ToInt32(Console.ReadLine());
            return new Electronics { productId = productId, productName = productName, description = description, price = price, quantityInStock = quantityInStock, type = type, brand = brand, warrantyPeriod = warrantyPeriod };
        }
        else if (type.Equals("Clothing", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Enter Size: ");
            string size = Console.ReadLine();
            Console.Write("Enter Color: ");
            string color = Console.ReadLine();
            return new Clothing { productId = productId, productName = productName, description = description, price = price, quantityInStock = quantityInStock, type = type, size = size, color = color };
        }
        else
        {
            throw new Exception("Invalid product type.");
        }
    }

    private static List<Product> GetProductListInput()
    {
        List<Product> productList = new List<Product>();
        Console.Write("How many products to order? ");
        int count = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Product product = GetProductInput();
            productList.Add(product);
        }

        return productList;
    }
}

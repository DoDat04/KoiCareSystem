using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class MyStoreContext
    {
        public static List<Category> Categories = new List<Category>();
        public static List<Product> Products = new List<Product>();
        public static List<Cart> Carts = new List<Cart>();
        public static List<Order> Orders = new List<Order>();

        static MyStoreContext()
        {
            // Adding sample categories for koi-related products
            var foodCategory = new Category { CategoryId = 1, CategoryName = "Food" };
            var toolsCategory = new Category { CategoryId = 2, CategoryName = "Tools" };
            var waterCategory = new Category { CategoryId = 3, CategoryName = "Water Treatments" };
            var accessoriesCategory = new Category { CategoryId = 4, CategoryName = "Accessories" };

            Categories.Add(foodCategory);
            Categories.Add(toolsCategory);
            Categories.Add(waterCategory);
            Categories.Add(accessoriesCategory);

            Products.Add(new Product
            {
                ProductId = 1,
                ProductName = "Koi Food Pellets",
                CategoryId = foodCategory.CategoryId,
                UnitsInStock = 150,
                UnitPrice = 29.99m,
                Description = "High-quality pellets for Koi fish, providing essential nutrients.",
                Category = foodCategory
            });

            Products.Add(new Product
            {
                ProductId = 2,
                ProductName = "Koi Wheat Germ",
                CategoryId = foodCategory.CategoryId,
                UnitsInStock = 100,
                UnitPrice = 34.99m,
                Description = "Nutritious wheat germ pellets ideal for Koi during cool months.",
                Category = foodCategory
            });

            Products.Add(new Product
            {
                ProductId = 3,
                ProductName = "Pond Water Testing Kit",
                CategoryId = toolsCategory.CategoryId,
                UnitsInStock = 60,
                UnitPrice = 19.99m,
                Description = "Comprehensive kit to monitor water quality in your pond.",
                Category = toolsCategory
            });

            Products.Add(new Product
            {
                ProductId = 4,
                ProductName = "Pond Vacuum Cleaner",
                CategoryId = toolsCategory.CategoryId,
                UnitsInStock = 25,
                UnitPrice = 149.99m,
                Description = "Powerful vacuum for cleaning debris from pond surfaces.",
                Category = toolsCategory
            });

            Products.Add(new Product
            {
                ProductId = 5,
                ProductName = "Algae Control Solution",
                CategoryId = waterCategory.CategoryId,
                UnitsInStock = 80,
                UnitPrice = 24.99m,
                Description = "Effective solution to control algae growth in ponds.",
                Category = waterCategory
            });

            Products.Add(new Product
            {
                ProductId = 6,
                ProductName = "Water Conditioner",
                CategoryId = waterCategory.CategoryId,
                UnitsInStock = 50,
                UnitPrice = 14.99m,
                Description = "Conditioner to promote safe and healthy water for fish.",
                Category = waterCategory
            });

            Products.Add(new Product
            {
                ProductId = 7,
                ProductName = "Koi Pond Net",
                CategoryId = accessoriesCategory.CategoryId,
                UnitsInStock = 40,
                UnitPrice = 39.99m,
                Description = "Durable net for safely capturing Koi fish.",
                Category = accessoriesCategory
            });

            Products.Add(new Product
            {
                ProductId = 8,
                ProductName = "Floating Pond Fountain",
                CategoryId = accessoriesCategory.CategoryId,
                UnitsInStock = 15,
                UnitPrice = 249.99m,
                Description = "Beautiful floating fountain to enhance the aesthetics of your pond.",
                Category = accessoriesCategory
            });


            Orders.Add(new Order(1, 4) // OrderId = 1, MemberId = 4
            {
                CartItems = new List<Cart>
                {
                    new Cart(1, 1, Products[0], 2), // Koi Food Pellets
                    new Cart(2, 1, Products[1], 1), // Koi Wheat Germ
                    new Cart(3, 1, Products[2], 1)  // Pond Water Testing Kit
                }
            });

            Orders.Add(new Order(2, 4) // OrderId = 2, MemberId = 4
            {
                CartItems = new List<Cart>
                {
                    new Cart(4, 1, Products[3], 1), // Pond Vacuum Cleaner
                    new Cart(5, 1, Products[4], 3)  // Algae Control Solution
                }
            });

            Orders.Add(new Order(3, 4) // OrderId = 3, MemberId = 4
            {
                CartItems = new List<Cart>
                {
                    new Cart(6, 2, Products[5], 2), // Water Conditioner
                    new Cart(7, 2, Products[6], 1)  // Koi Pond Net
                }
            });
        }
    }
}
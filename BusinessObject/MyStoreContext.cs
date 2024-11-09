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

            // Adding sample products
            Products.Add(new Product
            {
                ProductId = 1,
                ProductName = "Koi Food Pellets",
                CategoryId = foodCategory.CategoryId,
                UnitsInStock = 150,
                UnitPrice = 29.99m,
                Category = foodCategory
            });

            Products.Add(new Product
            {
                ProductId = 2,
                ProductName = "Koi Wheat Germ",
                CategoryId = foodCategory.CategoryId,
                UnitsInStock = 100,
                UnitPrice = 34.99m,
                Category = foodCategory
            });

            Products.Add(new Product
            {
                ProductId = 3,
                ProductName = "Pond Water Testing Kit",
                CategoryId = toolsCategory.CategoryId,
                UnitsInStock = 60,
                UnitPrice = 19.99m,
                Category = toolsCategory
            });

            Products.Add(new Product
            {
                ProductId = 4,
                ProductName = "Pond Vacuum Cleaner",
                CategoryId = toolsCategory.CategoryId,
                UnitsInStock = 25,
                UnitPrice = 149.99m,
                Category = toolsCategory
            });

            Products.Add(new Product
            {
                ProductId = 5,
                ProductName = "Algae Control Solution",
                CategoryId = waterCategory.CategoryId,
                UnitsInStock = 80,
                UnitPrice = 24.99m,
                Category = waterCategory
            });

            Products.Add(new Product
            {
                ProductId = 6,
                ProductName = "Water Conditioner",
                CategoryId = waterCategory.CategoryId,
                UnitsInStock = 50,
                UnitPrice = 14.99m,
                Category = waterCategory
            });

            Products.Add(new Product
            {
                ProductId = 7,
                ProductName = "Koi Pond Net",
                CategoryId = accessoriesCategory.CategoryId,
                UnitsInStock = 40,
                UnitPrice = 39.99m,
                Category = accessoriesCategory
            });

            Products.Add(new Product
            {
                ProductId = 8,
                ProductName = "Floating Pond Fountain",
                CategoryId = accessoriesCategory.CategoryId,
                UnitsInStock = 15,
                UnitPrice = 249.99m,
                Category = accessoriesCategory
            });
        }
    }
}
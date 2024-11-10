using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace Repositories.PRODUCT
{
    public class ProductDAO
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(MyStoreContext.Products);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await Task.FromResult(MyStoreContext.Products.FirstOrDefault(r => r.ProductId == id));
        }

        public async Task CreateAsync(Product product)
        {
            product.ProductId = MyStoreContext.Products.Max(r => r.ProductId) + 1;
            MyStoreContext.Products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = MyStoreContext.Products.FirstOrDefault(r => r.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                // Update properties of the existing product
                existingProduct.ProductName = product.ProductName;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.UnitsInStock = product.UnitsInStock;
                existingProduct.UnitPrice = product.UnitPrice;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var product = MyStoreContext.Products.FirstOrDefault(r => r.ProductId == id);
            if (product != null)
            {
                MyStoreContext.Products.Remove(product);
            }
            await Task.CompletedTask;
        }
    }
}
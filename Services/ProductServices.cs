using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Repositories.PRODUCT;

namespace Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepositories _productRepository;

        public ProductServices()
        {
            _productRepository = new ProductRepositories();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async Task CreateAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product );
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> IsProductIdUnique(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product == null;
        }
    }
}

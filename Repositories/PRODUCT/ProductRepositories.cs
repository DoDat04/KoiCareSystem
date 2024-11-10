using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Repositories.PRODUCT
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly ProductDAO _productDAO;

        public ProductRepositories()
        {
            _productDAO = new ProductDAO();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productDAO.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productDAO.GetByIdAsync(id);
        }


        public async Task CreateAsync(Product product)
        {
            await _productDAO.CreateAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productDAO.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productDAO.DeleteAsync(id);
        }
    }
}

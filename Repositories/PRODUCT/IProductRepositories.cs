using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Repositories.PRODUCT
{
    public interface IProductRepositories
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task CreateAsync(Product player);
        Task UpdateAsync(Product player);
        Task DeleteAsync(int id);
    }
}

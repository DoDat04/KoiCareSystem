using BusinessObject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryDAO
    {
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            // Simulate asynchronous operation
            return await Task.FromResult(MyStoreContext.Categories.ToList());
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await Task.FromResult(MyStoreContext.Categories.FirstOrDefault(t => t.CategoryId == id));
        }

        public async Task CreateAsync(Category category)
        {
            // Assign a new ID based on the maximum current ID
            category.CategoryId = MyStoreContext.Categories.Any() ? MyStoreContext.Categories.Max(t => t.CategoryId) + 1 : 1; // Start from 1 if no categories exist
            MyStoreContext.Categories.Add(category);
            await Task.CompletedTask; // No asynchronous operation needed here
        }

        public async Task UpdateAsync(Category category)
        {
            var existingCategory = MyStoreContext.Categories.FirstOrDefault(t => t.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                // If you have more properties to update, add them here
            }
            await Task.CompletedTask; // No asynchronous operation needed here
        }

        public async Task DeleteAsync(int id)
        {
            var category = MyStoreContext.Categories.FirstOrDefault(t => t.CategoryId == id);
            if (category != null)
            {
                MyStoreContext.Categories.Remove(category);
            }
            await Task.CompletedTask; // No asynchronous operation needed here
        }
    }
}
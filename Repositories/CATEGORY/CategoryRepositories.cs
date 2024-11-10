using BusinessObject;

namespace Repositories.CATEGORY
{
    public class CategoryRepositories : ICategoryRepositories
    {
        private readonly CategoryDAO _categoryDAO;

        public CategoryRepositories()
        {
            _categoryDAO = new CategoryDAO();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryDAO.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryDAO.GetByIdAsync(id);
        }

        public async Task CreateAsync(Category category)
        {
            await _categoryDAO.CreateAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryDAO.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryDAO.DeleteAsync(id);
        }
    }
}
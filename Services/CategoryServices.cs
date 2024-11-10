using BusinessObject;
using Repositories.CATEGORY;

namespace Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepositories _categoryRepository;

        public CategoryServices()
        {
            _categoryRepository = new CategoryRepositories();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Category category)
        {
            await _categoryRepository.CreateAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
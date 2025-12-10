using E_CommerceAPIs.Models.Entities;

namespace E_CommerceAPIs.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
    }
}

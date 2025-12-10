using E_CommerceAPIs.Data;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPIs.Repository
{
    public class CategoryRepository : DbProvider, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public Task<List<Category>> GetAllAsync()
        {
           return _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(i => i.CategoryId == id);
        }
    }
}

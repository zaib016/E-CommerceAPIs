using E_CommerceAPIs.Data;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPIs.Repository
{
    public class ProductRepository : DbProvider, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null) return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public Task<List<Product>> GetAllAsync()
        {
            return _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null) return null;

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}

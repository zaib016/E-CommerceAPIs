using E_CommerceAPIs.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPIs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
    public class DbProvider 
    {
        protected readonly ApplicationDbContext _dbContext;

        public DbProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }

}

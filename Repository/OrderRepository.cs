using E_CommerceAPIs.Data;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPIs.Repository
{
    public class OrderRepository : DbProvider, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order> AddAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(i => i.OrderId == id);
        }

        public async Task<Order?> GetOrderUserByIdAsync(int id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}

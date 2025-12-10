using E_CommerceAPIs.Models.Entities;

namespace E_CommerceAPIs.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<Order?> GetOrderUserByIdAsync(int id);
        Task<Order?> GetOrderByIdAsync(int id);

    }

}

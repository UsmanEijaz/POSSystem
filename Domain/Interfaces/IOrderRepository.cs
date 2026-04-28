using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<bool> RemoveOrder(long id);
        Task<Order?> GetOrderByIdAsync(long id);
    }

}

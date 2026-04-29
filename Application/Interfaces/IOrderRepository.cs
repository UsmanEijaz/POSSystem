using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderResponseDto> AddAsync(OrderRequest order);
        Task<bool> RemoveOrder(long id);
        Task<Order?> GetOrderByIdAsync(long id);
    }

}

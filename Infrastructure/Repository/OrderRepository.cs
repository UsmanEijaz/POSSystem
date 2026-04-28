using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
           
        }

        public async Task<Order?> GetOrderByIdAsync(long id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<bool> RemoveOrder(long id)
        {
            var order  = _context.Orders.Find(id);
            if (order != null) 
            {
                order.IsActive = false;
                order.ModifiedBy = 1;
                order.ModifiedDate = DateTime.Now;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

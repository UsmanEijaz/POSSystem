using Application.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<OrderResponseDto> AddAsync(OrderRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                long? findCustomerId = 0;
                if (request.Customer != null)
                {
                    var customer = await _context.Customers.FirstOrDefaultAsync(c=>c.Phone == request.Customer.Phone);
                    if (customer == null) 
                    {
                        var newCustomer = new Customer
                        {
                            FirstName = request.Customer.FirstName,
                            LastName = request.Customer.LastName,
                            Phone = request.Customer.Phone,
                            Email = request.Customer.Email,
                            IsActive = true,
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now,
                        };
                        _context.Customers.Add(newCustomer);
                        await _context.SaveChangesAsync();
                        findCustomerId = newCustomer.Id;
                    }
                    else
                    findCustomerId = customer.Id;
                }

                var order = new Order
                {
                    CustomerId = findCustomerId,
                    RegisterId = request.RegisterId,
                    CreatedDate = DateTime.Now,
                    OrderItems = new List<OrderItem>()
                };

                decimal total = 0;

                foreach (var item in request.OrderItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                        throw new Exception($"Product ID {item.ProductId} not found.");

                    if (product.Stock < item.Quantity)
                        throw new Exception($"Insufficient stock for {product.Name}. Available: {product.Stock}");

                    product.Stock -= item.Quantity;
                    product.ModifiedBy = 1;
                    product.ModifiedDate = DateTime.Now;

                    var orderItem = new OrderItem
                    {
                        ProductId = product.Id,
                        Price = product.Price, // Use the price from the Product table
                        Quantity = item.Quantity
                    };
                    
                    total += (orderItem.Price * orderItem.Quantity);
                    order.OrderItems.Add(orderItem);
                }

                order.TotalAmount = total;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                

                return new OrderResponseDto { IsSuccess = true, Message = "Order created successfully", TotalAmount = total,/*CustomerName = ,OrderItems = order.OrderItems, CustomerEmail = ""*/   };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new OrderResponseDto { IsSuccess = false, Message = ex.Message };
            }
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

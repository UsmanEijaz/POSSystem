using System.Text;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        private readonly EmailService _emailService;
        public OrderService(IOrderRepository repo, EmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            order.OrderNumber = GenerateOrderNumber();

            var saved = await _repo.AddAsync(order);

            string slip = GenerateSlip(saved);

            await _emailService.SendEmailAsync(saved.Email,
                "Order Confirmation",
                $"<h2>Order #{order.OrderNumber}</h2><p>Thank you for shopping!</p>",
                slip);

            return saved;
        }

        private string GenerateOrderNumber()
        {
            return "ORD-" + DateTime.UtcNow.Ticks;
        }

        private string GenerateSlip(Order order)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Order No: {order.OrderNumber}");
            sb.AppendLine($"Customer: {order.CustomerName}");
            sb.AppendLine("Items:");

            foreach (var item in order.Items)
                sb.AppendLine($"{item.ProductName} x{item.Quantity}");

            sb.AppendLine($"Total: {order.TotalAmount}");

            return sb.ToString();
        }
    }
}

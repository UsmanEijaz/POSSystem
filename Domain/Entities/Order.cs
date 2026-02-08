
namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}

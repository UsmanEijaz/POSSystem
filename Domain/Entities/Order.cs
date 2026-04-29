namespace Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public long OrderStatus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public long RegisterId { get; set; }
        public Register Register { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}

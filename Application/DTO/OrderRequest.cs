namespace Application.DTO
{
    public class OrderRequest
    {
        public CustomerDto? Customer { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public string OrderNumber { get; set; }
        public string PaymentMethod { get; set; } = "Cash";
        public int RegisterId {  get; set; }
    }
}

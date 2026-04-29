namespace Application.DTO
{
    public class OrderResponseDto : ResponseDto
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount {  get; set; }
        public string OrderNumber { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}

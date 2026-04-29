namespace Domain.Entities
{
    public class Register
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RegisterCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

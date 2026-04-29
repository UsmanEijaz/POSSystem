namespace Domain.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string StoreCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public ICollection<Register> Registers { get; set; } = new List<Register>();
    }
}

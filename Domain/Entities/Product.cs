namespace Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public decimal Price { get; set; } // Current price on shelf
        public decimal Stock { get; set; } // Current quantity available
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }
}

namespace Shop.Dto
{
    public class AddProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? Voucher { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public bool? IsDelete { get; set; }
        public string? Tag { get; set; }
        public long CategoryId { get; set; }
    }
}

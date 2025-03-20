using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? Voucher { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public bool? IsDelete { get; set; }
        public string? Tag { get; set; }
        public long CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<ReviewProduct> Reviews { get; set; } = new List<ReviewProduct>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

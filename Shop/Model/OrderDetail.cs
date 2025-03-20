using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long CartId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual Cart Cart { get; set; }
        public bool? IsDelete { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Voucher { get; set; }
        public double TotalPrice => Price * (1 - Voucher/100) * Quantity;
    }
}


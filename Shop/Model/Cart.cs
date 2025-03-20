using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class Cart
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

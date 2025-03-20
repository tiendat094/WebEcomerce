using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class ReviewProduct
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public int Ratings { get; set; }
        public string Review { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

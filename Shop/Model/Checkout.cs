
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Shop.Model
{
    public class Checkout
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName {  get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string EmailAddres { get; set; }
        public string Phone { get; set; }
        public DateTime DueDate { get; set; }
       // public double Total => OrderDetails.Sum(x => x.TotalPrice);
        public long InvoiceId { get; set; }
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string OrderDetailIds { get; set; } = "[]";

        // Không lưu trong database, chỉ dùng để thao tác dữ liệu trong code
        [NotMapped]
        public List<long> OrderDetailId
        {
            get => string.IsNullOrEmpty(OrderDetailIds) ? new List<long>() : JsonSerializer.Deserialize<List<long>>(OrderDetailIds);
            set => OrderDetailIds = JsonSerializer.Serialize(value);
        }
    }
}
    
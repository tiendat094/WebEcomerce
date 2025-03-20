using Microsoft.EntityFrameworkCore;
using Shop.Model;

namespace Shop.Context
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) :base(options){ }

        public DbSet<User> users { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<OrderDetail> ordersDetail { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ReviewProduct> reviews { get; set; }
        public DbSet<Checkout> checkouts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}

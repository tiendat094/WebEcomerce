using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Context;
using Shop.Model;

namespace Shop.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly ShopDbContext _context;
        public ProductDetailModel(ShopDbContext context)
        {
            _context = context;
        }
        public List<Product> Products = new List<Product>();
        public List<Product> RelateProducts = new List<Product>();
        public List<Category> Categories = new List<Category>();
        public List<UserProduct> Reviews = new List<UserProduct>();
        public double TotalCart { get; set; }
        public int QuantityCart { get; set; }
        [BindProperty]
        public double? aveRate {  get; set; }
        public Product product { get; set; }

        public IActionResult OnGet(int id, string searchText)
        {
            Categories = _context.categories.ToList();
            Products = _context.products.Include(x => x.Category).ToList();
            RelateProducts = _context.products.Where(x => x.CategoryId == 1).ToList();
            product = _context.products.FirstOrDefault(x => x.Id == id);
            Reviews = _context.reviews.Where(x => x.ProductId == id)
                .Select(s => new UserProduct
                {
                    User = _context.users.Where(a => a.Id==s.UserId).FirstOrDefault(),
                    Review = s
                }).ToList();
            if(Reviews.Count() > 0)
            {
                  aveRate = Reviews.Sum(x => x.Review.Ratings) / Reviews.Count();
            }
          
            if (!string.IsNullOrEmpty(searchText))
            {
                Products = _context.products
                    .Include(x => x.Category)
                    .Where(x => x.Name.ToLower().Contains(searchText.ToLower()))
                    .ToList();
            }

            return Page();
        }

        public IActionResult OnPostSearchProductAsync(string text)
        {
            return RedirectToPage(new { searchText = text });
        }
        [BindProperty]
        public ReviewProduct reviewProduct { get; set; } = new ReviewProduct();
        public IActionResult OnPostSubReviewAsync()
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            ReviewProduct review = new ReviewProduct();
            review.ProductId = reviewProduct.ProductId;
            review.UserId = userIdCurrent;
            review.Review = reviewProduct.Review;
            review.CreateTime = DateTime.UtcNow;
            review.Ratings = reviewProduct.Ratings;
            _context.reviews.Add(review);
            _context.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPost(long productId)
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            var cartCurrent = _context.carts
                .Include(x => x.OrderDetails)
                .FirstOrDefault(x => x.UserId == userIdCurrent);

            var product = _context.products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return RedirectToPage("/Shop", new { error = "San pham không ton tai!" });
            }

            if (cartCurrent != null)
            {
                var orderDetail = cartCurrent.OrderDetails.FirstOrDefault(x => x.ProductId == productId);

                if (orderDetail != null)
                {
                    orderDetail.Quantity += 1;
                }
                else
                {
                    orderDetail = new OrderDetail
                    {
                        ProductId = productId,
                        Quantity = 1,
                        Price = product.Price,
                        CartId = cartCurrent.Id,
                        Voucher = product.Voucher ?? 0
                    };
                    cartCurrent.OrderDetails.Add(orderDetail);
                }

                _context.carts.Update(cartCurrent);
            }
            else
            {
                var newCart = new Cart
                {
                    UserId = userIdCurrent,
                    OrderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    ProductId = productId,
                    Quantity = 1,
                    Price = product.Price,
                    Voucher = product.Voucher ?? 0
                }
            }
                };
                _context.carts.Add(newCart);
            }
            TotalCart = cartCurrent.OrderDetails.Sum(x => x.TotalPrice);
            QuantityCart = cartCurrent.OrderDetails.Count();
            HttpContext.Session.SetString("TotalCart", cartCurrent.ToString());
            HttpContext.Session.SetString("QuantityOrder", cartCurrent.ToString());
            _context.SaveChanges();
            return RedirectToPage("/Shop", new { success = "Thêm vào gi? hàng thành công!" });
        }
    }
    public class UserProduct
    {
        public User User { get; set; }
        public ReviewProduct Review
        {
            set; get;
        }
    }
}
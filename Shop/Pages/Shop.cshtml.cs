using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Context;
using Shop.Model;
using System.Net.Http.Headers;

namespace Shop.Pages
{
    public class ShopModel : PageModel
    {
        private readonly ShopDbContext _context;
        public ShopModel(ShopDbContext context)
        {
            _context = context;
        }
        public List<Product> Products = new List<Product>();
        public List<Product> RelateProducts = new List<Product>();
        public List<Category> Categories = new List<Category>();
        public Product Product = new Product();
        public double TotalCart { get; set; }
        public int QuantityCart { get; set; }
        public void OnGet(int? id)
        {
            Categories = _context.categories.ToList();
            if(id == null)
            {
                 Products = _context.products.ToList();
            }
            else
            {
                Products = _context.products.Where(x => x.CategoryId == id).ToList();
            }
            
        }

        public IActionResult OnPost(long productId)
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));

            // Load Cart với OrderDetails
            var cartCurrent = _context.carts
                .Include(x => x.OrderDetails)
                .FirstOrDefault(x => x.UserId == userIdCurrent);

            var product = _context.products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return RedirectToPage("/Shop", new { error = "Sản phẩm không tồn tại!" });
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
            HttpContext.Session.SetString("TotalCart", TotalCart.ToString());
            HttpContext.Session.SetString("QuantityOrder", QuantityCart.ToString());
            _context.SaveChanges();
            return RedirectToPage("/Shop", new { success = "Thêm vào giỏ hàng thành công!" });
        }

    }
}

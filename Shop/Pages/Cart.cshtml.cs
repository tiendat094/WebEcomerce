using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Context;
using Shop.Dto;
using Shop.Model;

namespace Shop.Pages
{
    public class CartModel : PageModel
    {
        private readonly ShopDbContext _context;
        public CartModel(ShopDbContext context)
        {
            _context = context;
        }
        public List<Product> Products = new List<Product>();
        public List<Product> RelateProducts = new List<Product>();
        public List<Category> Categories = new List<Category>();
        public List<OrderDetail> OrderDetails = new List<OrderDetail>();
        public Product Product = new Product();
        public double TotalCart {  get; set; }
        public IActionResult OnGet(string searchText)
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            Products = _context.products.ToList();
            Categories = _context.categories.ToList();
            OrderDetails = _context.carts
     .Include(x => x.OrderDetails) 
     .FirstOrDefault(x => x.UserId == userIdCurrent)?
     .OrderDetails.ToList();
            TotalCart = OrderDetails.Sum(x => x.TotalPrice);
            RelateProducts = _context.products.Where(x => x.CategoryId == 1).ToList();
            if (!string.IsNullOrEmpty(searchText))
            {
                Products = _context.products
                    .Include(x => x.Category)
                    .Where(x => x.Name.ToLower().Contains(searchText.ToLower()))
                    .ToList();
            }
            RelateProducts = _context.products.Where(x => OrderDetails.Select(s => s.Product.CategoryId).ToList().Contains(x.CategoryId)).ToList();
            return Page();

        }
        public IActionResult OnPostSearchProductAsync(string text)
        {
            return RedirectToPage(new { searchText = text });
        }
        public async Task<IActionResult> OnPostUpdateCartAsync(List<int> OrderId, List<int> Quantity)
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            OrderDetails = _context.carts
     .Include(x => x.OrderDetails)
     .FirstOrDefault(x => x.UserId == userIdCurrent)?
     .OrderDetails.ToList();
            for (int i = 0; i < OrderId.Count; i++)
            {
                var order = OrderDetails.Where(x => x.Id == OrderId[i]).FirstOrDefault();
                if (order != null)
                {
                    order.Quantity = Quantity[i];                 
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetRemoveItemAsync(int orderId)
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            var cartCurrent = _context.carts
                       .Include(x => x.OrderDetails)
                       .FirstOrDefault(x => x.UserId == userIdCurrent);
            var order = cartCurrent.OrderDetails.Where(x => x.Id == orderId).FirstOrDefault();
            if (order != null)
            {   
                cartCurrent.OrderDetails.Remove(order);
                _context.carts.Update(cartCurrent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); // Load lại trang giỏ hàng
        }

        public async Task<IActionResult> OnPostCheckoutAsync(List<long> OrderId)
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            var listOrderDetail = _context.ordersDetail.Where(x => OrderId.Contains(x.Id)).ToList();
            HttpContext.Session.SetString("OrderDetail",JsonConvert.SerializeObject(listOrderDetail));  

            return RedirectToPage("/Checkout");
        }
    }
}

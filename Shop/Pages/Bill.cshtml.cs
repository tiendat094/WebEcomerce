using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Context;
using Shop.Model;

namespace Shop.Pages
{
    public class BillModel : PageModel
    {
        private readonly ShopDbContext _context;
        public BillModel(ShopDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Checkout Checkout { get; set; }
        [BindProperty]
        public User user { get; set; }
        public List<Category> Categories { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        [BindProperty]
        public double TotalBill { get; set; }
        public double TotalCart { get; set; }
        public int QuantityCart { get; set; }
        public void OnGet()
        {
            Categories = _context.categories.ToList();
            var checkout = HttpContext.Session.GetString("Bill");
            Checkout = JsonConvert.DeserializeObject<Checkout>(checkout);
            user = _context.users.FirstOrDefault(x => x.Id == Checkout.UserId);
            OrderDetails = _context.ordersDetail.Include(s => s.Product).Where(x => Checkout.OrderDetailId.Contains(x.Id)).ToList();
            TotalBill = OrderDetails.Sum(x => x.TotalPrice);
        }
        public IActionResult OnPostFinishAsync()
        {
            var checkout = HttpContext.Session.GetString("Bill");
            Checkout = JsonConvert.DeserializeObject<Checkout>(checkout);
            var orderDetailIds = Checkout.OrderDetailId;
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            var cart = _context.carts
     .Include(x => x.OrderDetails)
     .FirstOrDefault(x => x.UserId == userIdCurrent);
            var listOrder = cart.OrderDetails.Where(x => orderDetailIds.Contains(x.Id)).ToList();
            var listProduct = _context.products.Where(x => listOrder.Select(x => x.ProductId).Contains(x.Id)).ToList();
            foreach(var pro in listProduct)
            {
                var order = listOrder.Where(x => x.ProductId == pro.Id).FirstOrDefault();
                pro.Quantity = pro.Quantity - order.Quantity;
                _context.products.Update(pro);
                _context.SaveChanges();
            }
            cart.OrderDetails = cart.OrderDetails.Where(x => !orderDetailIds.Contains(x.Id)).ToList();
            TotalCart = cart.OrderDetails.Sum(x => x.TotalPrice);
            QuantityCart = cart.OrderDetails.Count();
            HttpContext.Session.SetString("TotalCart",TotalCart.ToString());
            HttpContext.Session.SetString("QuantityOrder", QuantityCart.ToString());
            
            _context.carts.Update(cart);
            _context.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}

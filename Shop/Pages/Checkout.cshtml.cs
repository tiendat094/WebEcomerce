using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Context;
using Shop.Model;

namespace Shop.Pages
{
    public class CheckoutModel : PageModel
    {
		private readonly ShopDbContext _context;
		public CheckoutModel(ShopDbContext context)
		{
			_context = context;
		}
		public List<Product> Products = new List<Product>();
		public List<Product> RelateProducts = new List<Product>();
		public List<Category> Categories = new List<Category>();
        public List<OrderDetail > OrderDetails = new List<OrderDetail>();
		public Product Product = new Product();
        [BindProperty]
        public Checkout Checkout { get; set; }
        [BindProperty]
        public double totalCheckout {  get; set; }
        [BindProperty]
        public string Password { get; set; }
		public IActionResult OnGet(string searchText)
		{
            var orderDetailJson = HttpContext.Session.GetString("OrderDetail");
            if (!string.IsNullOrEmpty(orderDetailJson))
            {
                OrderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(orderDetailJson);
            }
            totalCheckout = OrderDetails.Sum(x => x.TotalPrice);
            Products = _context.products.ToList();
			Categories = _context.categories.ToList();
			RelateProducts = _context.products.Where(x => x.CategoryId == 1).ToList();
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

        public IActionResult OnPostCheckout()
        {
            var userIdCurrent = int.Parse(HttpContext.Session.GetString("userIdCurrent"));
            var user = _context.users.FirstOrDefault(x => x.Id == userIdCurrent);
            if (user.Password != Password)
            {
                return BadRequest("Mạt khau khong trung khop");
            }
            var orderDetailJson = HttpContext.Session.GetString("OrderDetail");
            OrderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(orderDetailJson);
            var checkout = new Checkout();
            checkout.Country = Checkout.Country;
            checkout.FirstName = Checkout.FirstName;
            checkout.LastName = Checkout.LastName;
            checkout.CompanyName = Checkout.CompanyName;
            checkout.City = Checkout.City;
            checkout.Address = Checkout.Address;
            checkout.EmailAddres = Checkout.EmailAddres;
            checkout.Phone = Checkout.Phone;
            checkout.UserId = userIdCurrent;
            checkout.OrderDetailId = OrderDetails.Select(x => x.Id).ToList();
            checkout.InvoiceId = new Random().Next(100000, 999999);
            _context.checkouts.Add(checkout);
            _context.SaveChanges();
            HttpContext.Session.SetString("Bill",JsonConvert.SerializeObject(checkout));
            return RedirectToPage("/Bill");
        }
    }
}

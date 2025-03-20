using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Context;

namespace Shop.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ShopDbContext _context;
        public LoginModel(ShopDbContext context)
        {
            _context = context;
        }

        public class UserLogin { 
          public string Email { get; set; }
          public string Password { get; set; }
        }
        [BindProperty]
        public UserLogin Input { set; get; } = new UserLogin();
         
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var isCheck = _context.users.Where(x => x.EmailAddress == Input.Email)
                .Where(x => x.Password == Input.Password).FirstOrDefault();
            var cartCurrent = _context.carts
               .Include(x => x.OrderDetails)
               .FirstOrDefault(x => x.UserId == isCheck.Id);
            var TotalCart = cartCurrent.OrderDetails.Sum(x => x.TotalPrice).ToString();
            var QuantityOrder = cartCurrent.OrderDetails.Count().ToString();
            if (isCheck == null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is incorrect.");
                return Page();
            }
            if(Input.Email == "Admin@ncc.asia" && Input.Password == "123456")
            {
                HttpContext.Session.SetString("isCheckAdmin", "true");
            }
            HttpContext.Session.SetString("isCheckLogin", "true");
            HttpContext.Session.SetString("TotalCart", TotalCart);
            HttpContext.Session.SetString("QuantityOrder", QuantityOrder);
            HttpContext.Session.SetString("userIdCurrent", isCheck.Id.ToString());
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetForgotPasswordAsync()
        {
            return RedirectToPage("/PagePressEmail");
        }
    }
}

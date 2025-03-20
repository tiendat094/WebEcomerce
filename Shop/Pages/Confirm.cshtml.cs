using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Context;
using Shop.Model;

namespace Shop.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly ShopDbContext _context;
        public ConfirmModel(ShopDbContext context)
        {
            _context = context;
        }
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _context.categories.ToList();
        }
        [BindProperty]
        public int Code { get; set; }
        public async Task<IActionResult> VerifyOtp()
        {
            var code = HttpContext.Session.GetString("Code");
            if(int.Parse(code) == Code) {
                return RedirectToPage();
            }
            return Page();
        }

    }
}

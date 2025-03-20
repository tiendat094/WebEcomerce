using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Context;
using Shop.Model;

namespace Shop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;
        public IndexModel(ShopDbContext context)
        {
            _context = context;
        }
        public bool IsCheckLogin { get; set; }  = false;
        public List<Category> Categories { get; set; } = new List<Category>();
        public void OnGet()
        {
            Categories = _context.categories.ToList();
            if (TempData["isCheckLogin"] != null)
            {
                IsCheckLogin = (bool)TempData["isCheckLogin"];
            }
        }

        
    }
}

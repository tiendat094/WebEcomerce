using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Context;
using Shop.Model;

namespace Shop.Pages.Shared
{
    public class _HeaderModel : PageModel
    {
        private readonly ShopDbContext _context;
        public _HeaderModel(ShopDbContext context)
        {
            _context = context;
        }
       
    }
}

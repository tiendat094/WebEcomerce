using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Shop.Context;
using Shop.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Pages
{
    public class MyAcountModel : PageModel
    {
        private readonly ShopDbContext _context;

        public MyAcountModel(ShopDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdCurrent = HttpContext.Session.GetString("userIdCurrent");
            if (string.IsNullOrEmpty(userIdCurrent))
            {
                return RedirectToPage("/Login");
            }
            Categories = _context.categories.ToList();
            var userId = long.Parse(userIdCurrent);
            User = _context.users.Where(x => x.Id == userId).FirstOrDefault();
            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateUserAsync()
        {

            var userIdCurrent = HttpContext.Session.GetString("userIdCurrent");
            if (string.IsNullOrEmpty(userIdCurrent))
            {
                return RedirectToPage("/Login");
            }

            var userId = long.Parse(userIdCurrent);
            var userToUpdate = await _context.users.FindAsync(userId);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin từ User được bind từ form
            userToUpdate.UserName = User.UserName;
            userToUpdate.EmailAddress = User.EmailAddress;
            userToUpdate.PhoneNumber = User.PhoneNumber;
            if (User.BirthDay.HasValue)
            {
                userToUpdate.BirthDay = DateTime.SpecifyKind(User.BirthDay.Value, DateTimeKind.Utc);
            }
            userToUpdate.AvatarUrl = User.AvatarUrl;

            _context.users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return RedirectToPage();
        }
    }
}

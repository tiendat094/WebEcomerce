using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Context;
using Shop.Model;
using System.ComponentModel.DataAnnotations;

namespace Shop.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ShopDbContext _context;
        public RegisterModel(ShopDbContext context)
        {
            _context = context;
        }
        public class RegisterUser
        {
            [Required]
        public string Name {  get; set; }
            [Required,DataType(DataType.Password)]
        public string Password {  get; set; }
            [Required,EmailAddress]
        public string Email { get; set; }
            [Required,DataType(DataType.Password),Compare(nameof(Password),ErrorMessage ="Passwords do not match")]  
            public string ConfirmPassword { get; set; }

        }
        [BindProperty]
        public RegisterUser Input { get; set; } = new RegisterUser();
        

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var listEmail = _context.users.Select(x => x.EmailAddress).ToList();
            var isCheck = listEmail.Contains(Input.Email);
            if (isCheck)
            {
                throw new Exception($"Acount used Email {Input.Email} is exits !");
            }

            User user = new User();
            user.EmailAddress = Input.Email;
            user.Password = Input.Password;
            user.UserName = Input.Name;
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Login");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using Shop.Context;
using Shop.Model;
using MailKit.Net.Smtp;

namespace Shop.Pages
{
    public class PagePressEmailModel : PageModel
    {
        private readonly ShopDbContext _context;
        public PagePressEmailModel(ShopDbContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _context.categories.ToList();
        }
        [BindProperty]
        public string EmailAddress { get; set; }
        public async Task<IActionResult> OnPostSendMailAsync()
        {
            HttpContext.Session.SetString("email", EmailAddress);
            var code = new Random().Next(1000,9999);
            HttpContext.Session.SetString("code", code.ToString());
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", "datd15287@gmail.com"));
            message.To.Add(new MailboxAddress("CLient", EmailAddress));
            message.Subject = "Ma code Reset Password";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Chào bạn!</h1><p>Đây là mã code gửi từ Nhoms6.Shop <h3>{code}</h3></p>"
            };

            // Cấu hình SMTP Server
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false); // Kết nối SMTP
                client.Authenticate("datd15287@gmail.com", "vjdtmdpkvhezznpv"); // Xác thực

                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToPage("/Confirm");
        }
    }
}

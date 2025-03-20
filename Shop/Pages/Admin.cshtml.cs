using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Context;
using Shop.Dto;
using Shop.Model;

namespace Shop.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ShopDbContext _context;
        public AdminModel(ShopDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string CategoryName { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var listCate = _context.categories.ToList();
            if(listCate.Select(x => x.Name).Contains(CategoryName))
            {
                throw new Exception("Category is exist");
            }
            var category = new Category { Name = CategoryName };
            _context.categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        
        public List<Product> listProduct = new List<Product>();
        public List<Category> listCategory = new List<Category>();
        public void OnGet()
        {
            listProduct = _context.products.ToList();
            listCategory = _context.categories.ToList();
        }

        

        [BindProperty] public AddProduct Input { set; get; } = new AddProduct();

        public async Task<IActionResult> OnPostCreateProductAsync()
        {
            var listProduct = _context.products.ToList();
            var product = listProduct.Where(x => x.Name == Input.Name).FirstOrDefault();
            if (product != null)
            {
                product.Name = Input.Name;
                product.Description = Input.Description;
                product.Price = Input.Price;
                product.Voucher = Input.Voucher;
                product.ImageUrl = Input.ImageUrl;
                product.Quantity = Input.Quantity;
                product.IsDelete = false;
                product.Tag = Input.Tag;
                product.CategoryId = Input.CategoryId;
                _context.products.Update(product);
            }
            else
            {
Product newProduct = new Product();
            newProduct.Name = Input.Name;
            newProduct.Description = Input.Description;
            newProduct.Price = Input.Price;
            newProduct.Voucher = Input.Voucher;
            newProduct.ImageUrl = Input.ImageUrl;
            newProduct.Quantity = Input.Quantity;
            newProduct.IsDelete = false;
            newProduct.CreatedDate = DateTime.UtcNow;
            newProduct.Tag = Input.Tag;
            newProduct.CategoryId = Input.CategoryId;
            _context.products.Add(newProduct);
            }
            
            _context.SaveChanges();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteProductAsync(int id)
        {
            var product =  _context.products.Where(x =>x.Id == id).FirstOrDefault();
            if (product == null) return NotFound();

            _context.products.Remove(product);
           _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }

    
}
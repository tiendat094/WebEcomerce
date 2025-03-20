using Shop.Context;

namespace Shop.Service
{
	public class ProductService
	{
		private readonly ShopDbContext _context;
		public ProductService(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<List<ProductDto>> GetProducts()
		{
			var listProduct = _context.products.Where(s => s.IsDelete != true).Select(x => new ProductDto
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description,
				ImageUrl = x.ImageUrl,
				Price = x.Price,
				Quantity = x.Quantity,	
				Tag = x.Tag,
				CategoryId = x.CategoryId,

			}).ToList();
			return listProduct;
		}

		public void DeleteProduct(long productId)
		{
			var product = _context.products.FirstOrDefault(x => x.Id == productId);
			product.IsDelete = true;
			_context.SaveChanges();
		}

		public void UpdateProduct(ProductDto product)
		{

		}
		public class ProductDto
		{
			public long Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public string ImageUrl { get; set; }
			public double Price { get; set; }
			public int Quantity { get; set; }
			public string Tag { get; set; }
			public long CategoryId { get; set; }
		}

	}
}

	



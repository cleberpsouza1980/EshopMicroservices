using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Catalog;
using ShoppingWeb.Services;

namespace ShoppingWeb.Pages.Products
{
    public class IndexModel(ICatalogService catalogService,ILogger<IndexModel> logger) : PageModel
    {        
        public IEnumerable<ProductModel> Products { get; set; } = new List<ProductModel>();
        
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited)");

            var result = await catalogService.GetProducts();

            if(SearchTerm is not null)
            {
                var product = result.Products.Where(p => p.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
                Products = product;

                return Page();
            }

            Products = result.Products;

            return Page();
        }
    }
}

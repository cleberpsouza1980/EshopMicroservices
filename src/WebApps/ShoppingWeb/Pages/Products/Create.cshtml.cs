using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Catalog;

namespace ShoppingWeb.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductModel _product;
        public ProductModel Product { get; set; }

        public CreateModel(ProductModel product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            return RedirectToPage("./Index");
        }
    }
}
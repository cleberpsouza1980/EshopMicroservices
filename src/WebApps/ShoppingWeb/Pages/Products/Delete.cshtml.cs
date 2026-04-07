using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Catalog;

namespace ShoppingWeb.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly Guid _id;
        public ProductModel Product { get; set; }
        public DeleteModel(Guid id)
        {
            _id = id;
        }

        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            return RedirectToPage("./Index");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Catalog;

namespace ShoppingWeb.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly int _id;
        public ProductModel Product { get; set; }

        public EditModel(int id)
        {
            _id = id;
        }


        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            if (Product == null)
            {
                return NotFound();
            }

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
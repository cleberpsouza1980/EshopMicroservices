using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingWeb.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}

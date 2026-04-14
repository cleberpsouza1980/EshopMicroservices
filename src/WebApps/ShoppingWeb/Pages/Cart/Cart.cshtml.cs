using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Basket;
using ShoppingWeb.Services;

namespace ShoppingWeb.Pages.Cart
{
    public class CartModel(IBasketServices basketService,ILogger<CartModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();
        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid productId)
        {
            logger.LogInformation("Removing product with id {ProductId} from the cart", productId);

            Cart = await basketService.LoadUserBasket();
            Cart.Items.RemoveAll(i => i.ProductId == productId);

            return Page();
        }

        public async Task<IActionResult> OnPostClearCartAsync()
        {
            logger.LogInformation("Clearing the cart");

            var userName = "swn";

            await basketService.DeleteBasket(userName);
            
            return Page();
        }
    }
}

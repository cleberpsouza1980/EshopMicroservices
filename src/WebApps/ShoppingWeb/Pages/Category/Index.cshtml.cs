using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Basket;
using ShoppingWeb.Models.Catalog;
using ShoppingWeb.Services;
using System.ComponentModel;

namespace ShoppingWeb.Pages.Category
{
    public class IndexModel(ICatalogService catalogService, IBasketServices basketServices,
        ILogger<IndexModel> logger) : PageModel
    {

        public IEnumerable<string> CategoryList { get; set; } = [];
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var response = await catalogService.GetProducts();

            CategoryList = response.Products.SelectMany(p => p.Category).Distinct();


            if (!string.IsNullOrEmpty(categoryName))
            {
                ProductList = response.Products.Where(p => p.Category.Contains(categoryName));
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = response.Products;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Adding product with id {ProductId} to the cart", productId);
            var productResponse = await catalogService.GetProductById(productId);
            if (productResponse == null)
            {
                logger.LogWarning("Product with id {ProductId} not found", productId);
                return NotFound();
            }
            var cart = await basketServices.LoadUserBasket();
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                logger.LogInformation("Incremented quantity of product with id {ProductId} in the cart", productId);
            }
            else
            {
                cart.Items.Add(new ShoppingCarItem
                {
                    ProductId = productId,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quantity = 1
                });
                logger.LogInformation("Added product with id {ProductId} to the cart", productId);
            }
            await basketServices.StoreBasket(new StoreBasketRequest(cart));

            return RedirectToPage("/Cart/Cart");
        }
    }
}

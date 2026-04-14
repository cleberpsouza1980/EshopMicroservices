using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Models.Basket;
using ShoppingWeb.Models.Catalog;
using ShoppingWeb.Services;

namespace ShoppingWeb.Pages.Products
{
    public class DetailsModel(ICatalogService catalogService, IBasketServices basketServices
        , ILogger<IndexModel> logger) : PageModel
    {
        public ProductModel? Product { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var result = await catalogService.GetProductById(productId);

            if (result is null)
            {
                logger.LogWarning("Product with id {productId} not found", productId);
                return NotFound();
            }

            Product = result.Product;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            try
            {
                logger.LogInformation("Add to cart  the product " + productId);

                var productResponse = await catalogService.GetProductById(productId);

                var basket = await  basketServices.LoadUserBasket();

                basket.Items.Add(new ShoppingCarItem
                {
                    ProductId = productId,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quantity = 1,
                    Color = "Black",
                });

                await basketServices.StoreBasket(new StoreBasketRequest(basket));
                // Mensagem de sucesso (opcional)
                //TempData["Success"] = "Produto adicionado ao carrinho!";

                // Redireciona para a página do carrinho ou mantém na mesma página
                //return RedirectToPage("/Cart/Index");
                // Ou: return RedirectToPage(); // Para ficar na mesma página

                return RedirectToPage("/Cart/Cart");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro: {ex.Message}";
                return Page();
            }
        }

       
    }
}
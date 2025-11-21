
using Spectre.Console;

namespace CatalogAPI.Products.GetProducts
{
    //public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var products = await sender.Send(new GetProductsQuery());
                var response = new GetProductsResponse(products.Products);
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");

        }
    }
}

namespace CatalogAPI.Products.GetProductByCategory
{
    public class GetProductByCategoryResponse
    {
        public IEnumerable<Product> Products { get; set; }
    }
    public class GetProductByCatagoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var products = await sender.Send(new GetProductByCategoryQuery(category));
                var response = new GetProductByCategoryResponse
                {
                    Products = products.Products
                };
                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category");
        }
    }
}

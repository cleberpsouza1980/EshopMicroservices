namespace CatalogAPI.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductcsQueryHandler 
            (IDocumentSession session,ILogger<GetProductcsQueryHandler> logger)
            : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsQuery");
            var products = await session.Query<Product>()
                                        .ToListAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} products", products.Count);
            return new GetProductsResult(products);
        }
    }
}

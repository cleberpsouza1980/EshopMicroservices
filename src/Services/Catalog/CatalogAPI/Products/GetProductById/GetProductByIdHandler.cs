namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Handling GetProductByIdQuery for Id: {id}", query.id);
                var product = await session.LoadAsync<Product>(query.id, cancellationToken);
                if (product == null)
                {
                    logger.LogWarning("Product with Id: {Id} not found", query.id);
                    throw new ProductNotFoundException(query.id);
                }
                logger.LogInformation("Retrieved product with Id: {Id}", query.id);
                return new GetProductByIdResult(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while handling GetProductByIdQuery for Id: {Id}", query.id);
                throw;
            }

        }
    }
}

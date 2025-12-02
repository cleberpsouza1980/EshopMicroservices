namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {                
                var product = await session.LoadAsync<Product>(query.id, cancellationToken);
                if (product == null)
                {
                    throw new ProductNotFoundException(query.id);
                }
                return new GetProductByIdResult(product);
            }
            catch 
            {
                throw;
            }

        }
    }
}

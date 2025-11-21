using System.Linq;

namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public class GetProductByCategoryResult
    {
        public IEnumerable<Product> Products { get; }
        public GetProductByCategoryResult(IEnumerable<Product> products)
        {
            Products = products;
        }
    }
    internal class GetProductByCategoryHandler(IDocumentSession session,ILogger<GetProductByCategoryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
     public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Handling GetProductByCategoryQuery for Category: {category}", query);
                var products = await session.Query<Product>()
                                            .Where(p => p.Category.Contains(query.Category.ToString()))
                                            .ToListAsync(cancellationToken);
                logger.LogInformation("Retrieved {count} products for Category: {category}", products.Count, query);
                return new GetProductByCategoryResult(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while handling GetProductByCategoryQuery for Category: {category}", query);
                throw;
            }
        }
    }
}

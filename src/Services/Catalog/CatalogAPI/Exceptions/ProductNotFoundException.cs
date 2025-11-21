namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(Guid Id) : base($"Product {Id} not found.") { }
        
    }
}

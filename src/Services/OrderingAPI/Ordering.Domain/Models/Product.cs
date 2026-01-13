namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } =decimal.Zero;

        public static Product Create(ProductId id, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative or zero.");
            }

            var product = new Product
            {
                Id = id,
                Name = name,
                Price = price,
            };
            return product;
        }
    }
}

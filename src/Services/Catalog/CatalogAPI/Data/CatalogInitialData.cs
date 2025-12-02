using Marten.Schema;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfigureProducts());
            await session.SaveChangesAsync(cancellation);
        }

        public static IEnumerable<Product> GetPreconfigureProducts() =>
         new List<Product>()
        {
            new Product()
            {
                Id=new Guid(),
                Name = "IPhone X",
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Price = 950.00M,
                ImageFile = "product-1.png",
                Category = new List<string> {"Smart Phone" }
            },
            new Product()
            {
                Id=new Guid(),
                Name = "Samsung 10",
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Price = 840.00M,
                ImageFile = "product-2.png",
                Category = new List<string> { "Smart Phone" }
            },
            new Product()
            {
                Id=new Guid(),
                Name = "Huawei Plus",
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Price = 650.00M,
                ImageFile = "product-3.png",
                Category =  new List<string> {"White Appliances" }
            }
        };
    }
}

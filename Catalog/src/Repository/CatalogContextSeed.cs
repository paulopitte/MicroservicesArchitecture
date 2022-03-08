using Catalog.Api.Domain;
using MongoDB.Driver;

namespace Catalog.Api.Repository
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (existProduct)
                productCollection.InsertManyAsync(GetProducts());
        }

        private static IEnumerable<Product> GetProducts()
        {
            var prod = new List<Product>();
            prod.Add(new Product(new Random().Next(), "SKU_123", "IPhone 13", 12346.99M));
            prod.Add(new Product(new Random().Next(), "SKU_456", "IPhone 15", 22346.99M));
            prod.Add(new Product(new Random().Next(), "SKU_789", "IPhone 18", 32346.99M));

            return prod;
        }
    }
}

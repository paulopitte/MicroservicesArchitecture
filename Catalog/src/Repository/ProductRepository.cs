using Catalog.Api.Domain;
using MongoDB.Driver;

namespace Catalog.Api.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByTitle(string title);
        Task<IEnumerable<Product>> GetProductsByCategory(string name);
        Task<Product> GetBySkuAsync(string sku);
        Task<Product> GetProduct(string id);

        Task CreateAsync(Product product);
        Task<bool> Update(Product product);

        Task<bool> Delete(string id);
    }

    public class ProductRepository : IProductRepository
    {

        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ??
                throw new ArgumentException(nameof(context));
        }

        public async Task CreateAsync(Product product) =>
            await _context.Products
                  .InsertOneAsync(product)
                  .ConfigureAwait(false);


        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetBySkuAsync(string sku)
        {
            return await _context.Products.Find(p => p.Sku == sku).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProduct(string id) =>
            await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();


        public async Task<IEnumerable<Product>> GetProducts() =>
             await _context.Products
                                .Find(prop => true)
                                .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsByCategory(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByTitle(string title)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Title, title);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }


}

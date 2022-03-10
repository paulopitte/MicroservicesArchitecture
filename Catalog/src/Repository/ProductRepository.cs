using Catalog.Api.Domain;
using MongoDB.Driver;
using Sigc.Core.Caching.Core;
using Sigc.Core.Caching.Shared.Catalog;

namespace Catalog.Api.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(int channelId = 0);
        Task<IEnumerable<Product>> GetProductsByTitleAsync(string title);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string name);
        Task<Product> GetBySkuAsync(string sku, int channelId = 0);
        Task<Product> GetProductAsync(string id, int channelId = 0);

        Task SaveAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
        Task<bool> CheckExistsBySkuAsync(string sku, int channelId = 0);
        Task<bool> CheckExistsByIdAsync(string id, int channelId = 0);

        Task CleanProductCacheBySku(string sku, int channelId = 0);
    }

    public class ProductRepository : IProductRepository
    {

        private readonly ICatalogContext _context;
        private readonly IStaticCacheManager _staticCacheManager;

        public ProductRepository(ICatalogContext context, IStaticCacheManager staticCacheManager)
        {
            _context = context ??
                throw new ArgumentException(nameof(context));

            _staticCacheManager = staticCacheManager
               ?? throw new ArgumentException(nameof(staticCacheManager));
        }




        public async Task SaveAsync(Product product) =>
            await _context.Products
                  .InsertOneAsync(product)
                  .ConfigureAwait(false);





        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }





        public async Task<bool> UpdateAsync(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }




        /// <summary>
        /// Obtém um produto de acordo com o seu SKU.
        /// </summary>
        /// <param name="sku">Sku do produto.</param>
        /// <returns>Produto se encontrado.</returns>
        public async Task<Product> GetBySkuAsync(string sku, int channelId = 0)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductBySkuAndChannelIdCacheKey, sku, channelId);
            return await CacheControl(cacheKey).ConfigureAwait(false);

            async Task<Product> CacheControl(CacheKey cacheKey) =>
                    await _staticCacheManager.GetAsync(cacheKey, false, async () =>
                        await GetAsync());

            async Task<Product> GetAsync() =>
                    await _context.Products.Find(p => p.Sku == sku)
                    .FirstOrDefaultAsync();

        }


        public async Task<Product> GetProductAsync(string id, int channelId = 0)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductBySkuCacheKey, id, channelId);
            return await CacheControl(cacheKey).ConfigureAwait(false);

            async Task<Product> CacheControl(CacheKey cacheKey) =>
                    await _staticCacheManager.GetAsync(cacheKey, false, async () =>
                        await GetAsync());

            async Task<Product> GetAsync() =>
                await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsAsync(int channelId = 0)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductHasProductCacheKey, channelId);
            return await CacheControl(cacheKey).ConfigureAwait(false);

            async Task<IEnumerable<Product>> CacheControl(CacheKey cacheKey) =>
                    await _staticCacheManager.GetAsync(cacheKey, false, async () =>
                        await GetAsync());

            async Task<IEnumerable<Product>> GetAsync() =>
               await _context.Products
                                    .Find(prop => true)
                                    .ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByTitleAsync(string title)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Title, title);
            return await _context.Products.Find(filter).ToListAsync();
        }



        /// <summary>
        /// Identifica se o produto já existe.
        /// </summary>
        /// <param name="sku">Sku do produto</param>
        /// <param name="channelId">Identificador do canal.</param>
        /// <returns><value>true</value> se o produto existe.</returns>
        public async Task<bool> CheckExistsBySkuAsync(string sku, int channelId = 0)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductHasProductCacheKey, sku, channelId);
            return await _staticCacheManager.GetAsync(cacheKey, false, async () =>
                     await GetAsync(sku, channelId));


            async Task<bool> GetAsync(string sku, int channelId = 0)
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Sku, sku);
                return await _context.Products.CountAsync(filter) > 0;
            }
        }



        /// <summary>
        /// Identifica se o produto já existe buscanbo pelo ID.
        /// </summary>
        /// <param name="sku">Sku do produto</param>
        /// <param name="channelId">Identificador do canal.</param>
        /// <returns><value>true</value> se o produto existe.</returns>
        public async Task<bool> CheckExistsByIdAsync(string id, int channelId = 0)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductHasProductCacheKey, id, channelId);
            return await _staticCacheManager.GetAsync(cacheKey, false, async () =>
                     await GetAsync(id, channelId));


            async Task<bool> GetAsync(string id, int channelId = 0)
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);
                return await _context.Products.CountAsync(filter) > 0;
            }
        }



        /// <summary>
        /// Clean product cache by sku
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="channelId"></param>
        public async Task CleanProductCacheBySku(string sku, int channelId = 0)
        {
            _staticCacheManager.Remove(ProductCacheDefaults.ProductBySkuCacheKey, sku, channelId);
            _staticCacheManager.Remove(ProductCacheDefaults.ProductPriceBySkuCacheKey, sku, channelId);

            //// remove parent cache by key
            //var parentSku = await GetParentSkuAsync(sku, channelId);
            //if (string.IsNullOrEmpty(parentSku)) return;

            //var parentProduct = await GetProductBySkuAsync(parentSku, channelId);
            //if (parentProduct == null) return;

            //_staticCacheManager.Remove(ProductCacheDefaults.ProductBySkuCacheKey, parentProduct.Sku, channelId);
            //_staticCacheManager.Remove(ProductCacheDefaults.ProductPriceBySkuCacheKey, parentProduct.Sku, channelId);

            //// remove variations cache by key
            //var variations = await GetProductVariationsAsync(parentProduct.Id);
            //if (variations == null || !variations.Any()) return;

            //foreach (var variation in variations)
            //{
            //    _staticCacheManager.Remove(ProductCacheDefaults.ProductBySkuCacheKey, variation.Sku, channelId);
            //    _staticCacheManager.Remove(ProductCacheDefaults.ProductPriceBySkuCacheKey, variation.Sku,
            //        channelId);
            //}

            await Task.FromResult(true);
        }

    }


}

using Sigc.Core.Caching.Core;
using Sigc.Core.Caching.Shared.Catalog;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBacketRepository
    {
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ILogger<BasketRepository> _logger;
        public BasketRepository(IStaticCacheManager staticCacheManager, ILogger<BasketRepository> logger)
        {
            _staticCacheManager = staticCacheManager 
                ?? throw new ArgumentException(nameof(staticCacheManager));

            _logger = logger
             ?? throw new ArgumentException(nameof(logger));
        }

        public async  Task GetBasket(string userName)
        {
            throw new NotImplementedException();
        }

        private void RemoveCacheKey(string sku, int sellerId)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductHasProductCacheKey, sku, sellerId);
            _staticCacheManager.Remove(cacheKey);
        }

    }
}

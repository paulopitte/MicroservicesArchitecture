using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Sigc.Core.Caching.Core;

using System.Text.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IStaticCacheManager _staticCacheManager;

       
        private readonly ILogger<BasketRepository> _logger;
        private readonly IDistributedCache _redisCache;

        // public BasketRepository(
        //                         IStaticCacheManager staticCacheManager, 
        //                         ILogger<BasketRepository> logger)
        //                         {
        //                            _staticCacheManager = staticCacheManager 
        //                                ?? throw new ArgumentException(null, nameof(staticCacheManager));

        //                             _logger = logger
        //                              ?? throw new ArgumentException(null, nameof(logger));
        //                         }





        public BasketRepository(IDistributedCache redisCache, ILogger<BasketRepository> logger)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _logger = logger ?? throw new ArgumentException(null, nameof(logger));

        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            _logger.LogInformation("Obtendo chave Redis...");
            var basket = await _redisCache.GetAsync(userName);
            if (basket is null) return null;

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            _logger.LogInformation("Inserindo chave Redis...");

            await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
            return await GetBasket(basket.UserName);

        }

        private void RemoveCacheKey(string sku, int sellerId)
        {
            //var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductHasProductCacheKey, sku, sellerId);
            //_staticCacheManager.Remove(cacheKey);
        }

    }
}

using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Request = Core.Contracts.Requests;
using Sigc.MktHub.Catalog.Core.Application.Products.Queries;
using Catalog.Api.Applications.Products.Commands;
using Core.Contracts.Requests;

namespace Catalog.Api.Core.Application.Products.Services
{
    public interface IProductService
    {
        Task<ValidationResult> SaveAsync(Request.Product request, int channelId, Dictionary<string, string> headers);
        Task<ValidationResult> UpdateAsync(Request.Product request, int channelId = 0, Dictionary<string, string> headers = null);
        Task<ValidationResult> DeleteAsync(string id, int channelId, Dictionary<string, string> headers);


        Task<Request.Product> GetByIdAsync(string id, int channelId = 0);
        Task<Request.Product> GetBySkuAsync(string sku, int channelId = 0);
        Task<Request.Product> GetProductsByCategoryAsync(string category);
    }

    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        //private readonly IStaticCacheManager _staticCacheManager;
        private readonly ILogger<ProductService> _logger;


        public ProductService(IMediator mediator,
                            IMapper mapper,
                            //IStaticCacheManager staticCacheManager,
                            ILogger<ProductService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            //_staticCacheManager = staticCacheManager;
            _logger = logger;
        }






        public async Task<ValidationResult> SaveAsync(Request.Product request, int channelId, Dictionary<string, string> headers)
        {
            var command = _mapper.Map<ProductCreateCommand>(request);

            var validationResult = await _mediator.Send(command);

            if (validationResult.IsValid)
            {
                //TODO:  PUBLIC QUEUE
                //PUBLISH

                // REMOVE CACHE PARA NOVA PERSISTENCIA COM DADOS ATUALIZADOS
                // RemoveCacheKey(product.Sku);
                _logger.LogInformation("Apply bussiness rule and save product.");


            }
            return validationResult;
        }


        public async Task<ValidationResult> UpdateAsync(Request.Product request, int channelId, Dictionary<string, string> headers)
        {

            var command = _mapper.Map<ProductUpdateCommand>(request);
            var validationResult = await _mediator.Send(command);

            if (validationResult.IsValid)
            {
                //TODO:  PUBLIC QUEUE

                // REMOVE CACHE PARA NOVA PERSISTENCIA COM DADOS ATUALIZADOS
                //  RemoveCacheKey(product.Sku, product.ChannelId);
                _logger.LogInformation("Apply bussiness rule and save product.");


            }
            return validationResult;
        }


        public async Task<ValidationResult> DeleteAsync(string id, int channelId = 0, Dictionary<string, string> headers = null)
        {
            var validationResult = await _mediator.Send(new ProductDeleteCommand() { Id = id });
            if (validationResult.IsValid)
            {
                //TODO:  PUBLIC QUEUE / NOTIFICATION EVENTS ETC..


                // REMOVE CACHE PARA NOVA PERSISTENCIA COM DADOS ATUALIZADOS
                //  RemoveCacheKey(product.Sku, product.ChannelId);
                _logger.LogInformation("Apply bussiness rule and delete product.");


            }
            return validationResult;
        }


        public async Task<Request.Product> GetByIdAsync(string id, int channelId = 0) => _mapper.Map<Request.Product>(await _mediator.Send(new GetProductByIdQuery(id)));
        public async Task<Request.Product> GetBySkuAsync(string sku, int channelId = 0) => _mapper.Map<Request.Product>(await _mediator.Send(new GetProductBySkuQuery(sku)));

        public async Task<Product> GetProductsByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }






        //private void RemoveCacheKey(string sku, int sellerId)
        //{
        //    var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ProductCacheDefaults.ProductHasProductCacheKey, sku, sellerId);
        //    _staticCacheManager.Remove(cacheKey);
        //}

    }
}

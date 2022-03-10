using Catalog.Api.Applications.Products.Queries;
using Catalog.Api.Domain;
using Catalog.Api.Repository;
using MediatR;
using Sigc.MktHub.Catalog.Core.Application.Products.Queries;

namespace Sigc.MktHub.Catalog.Core.Application.Products.Handlers
{
    public class QueryHandlers : IRequestHandler<GetProductByIdQuery, Product>,
                                 IRequestHandler<GetProductBySkuQuery, Product>,
                                 IRequestHandler<GetCheckExistsBySkuQuery, bool>
    {

        //TODO CONSULTA MONGODB
        private readonly IProductRepository _productRepository;

        public QueryHandlers(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
                     await _productRepository.GetProductAsync(request.Id);

        public async Task<Product> Handle(GetProductBySkuQuery request,
                 CancellationToken cancellationToken) =>
                    await _productRepository.GetBySkuAsync(request.Sku);


        public async Task<bool> Handle(GetCheckExistsBySkuQuery request, CancellationToken cancellationToken) =>
            await _productRepository.CheckExistsAsync(request.Sku);


    }
}

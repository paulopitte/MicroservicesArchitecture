using Catalog.Api.Applications.Products.Queries;
using Catalog.Api.Domain;
using Catalog.Api.Repository;
using MediatR;

namespace Catalog.Api.Core.Application.Products.Handlers
{
    public class QueryHandlers : IRequestHandler<GetProductsQuery, IEnumerable<Product>>,
                                 IRequestHandler<GetProductByIdQuery, Product>,
                                 IRequestHandler<GetProductBySkuQuery, Product>,
                                 IRequestHandler<GetCheckExistsBySkuQuery, bool>
    {

        //TODO CONSULTA MONGODB
        private readonly IProductRepository _productRepository;

        public QueryHandlers(IProductRepository productRepository) =>
            _productRepository = productRepository
                ?? throw new ArgumentException(nameof(productRepository));




        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
                     await _productRepository.GetProductAsync(request.Id);

        public async Task<Product> Handle(GetProductBySkuQuery request,
                 CancellationToken cancellationToken) =>
                    await _productRepository.GetBySkuAsync(request.Sku);


        public async Task<bool> Handle(GetCheckExistsBySkuQuery request, CancellationToken cancellationToken) =>
            await _productRepository.CheckExistsBySkuAsync(request.Sku);

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
              await _productRepository.GetProductsAsync()
                                      .ConfigureAwait(false);

    }
}

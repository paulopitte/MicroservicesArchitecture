using Catalog.Api.Domain;
using MediatR;

namespace Catalog.Api.Applications.Products.Queries
{
    public struct GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public bool? State { get; set; } = true;

        public GetProductsQuery(bool? state = null) => 
            (State) = (state);
        
    }
}

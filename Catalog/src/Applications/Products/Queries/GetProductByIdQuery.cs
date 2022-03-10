using Catalog.Api.Domain;
using MediatR;
 
namespace Catalog.Api.Applications.Products.Queries
{
    public struct GetProductByIdQuery : IRequest<Product>
    {
        public string Id { get; set; }
        public string? ChannelId { get; set; }

        public GetProductByIdQuery(string id, string? channelId = null) =>
            (Id, ChannelId) = (id, channelId);
         
    }
}

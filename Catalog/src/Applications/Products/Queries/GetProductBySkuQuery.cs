using Catalog.Api.Domain;
using MediatR;
 
namespace Catalog.Api.Applications.Products.Queries
{
    public struct GetProductBySkuQuery : IRequest<Product>
    {
        public string Sku { get; set; }
        public int ChannelId { get; set; }
        public GetProductBySkuQuery(string sku, int channelId = 0) =>       
            (Sku, ChannelId) = (sku, channelId);
         
    }
}

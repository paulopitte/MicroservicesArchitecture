using MediatR;
 
namespace Catalog.Api.Applications.Products.Queries
{

    public class GetCheckExistsBySkuQuery : IRequest<bool>
    {
        public string Sku { get; set; }
        public GetCheckExistsBySkuQuery(string sku) =>      
            Sku = sku;
        
    }
}

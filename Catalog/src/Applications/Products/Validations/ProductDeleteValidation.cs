
namespace Catalog.Api.Applications.Products.Validations
{
    using Commands;

    public class ProductDeleteValidation : ProductValidation<ProductCommand>
    {
        public ProductDeleteValidation()
        {
            ValidationId();            
        }
    }
}

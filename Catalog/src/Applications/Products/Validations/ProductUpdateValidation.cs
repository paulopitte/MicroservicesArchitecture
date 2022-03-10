
namespace Catalog.Api.Applications.Products.Validations
{
    using Commands;

    public class ProductUpdateValidation : ProductValidation<ProductCommand>
    {
        public ProductUpdateValidation()
        {
            ValidationId();
            ValidationSku();
            ValidationTitle();
            ValidationPrice();
            ValidationPriceTwoDecimal();
        }
    }
}

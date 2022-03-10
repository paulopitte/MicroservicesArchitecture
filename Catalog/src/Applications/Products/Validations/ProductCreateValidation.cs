
namespace Catalog.Api.Applications.Products.Validations
{
    using Commands;

    public class ProductCreateValidation : ProductValidation<ProductCreateCommand>
    {
        public ProductCreateValidation()
        {
            ValidationSku();
            ValidationTitle();
            ValidationPrice();
            ValidationPriceTwoDecimal();
        }
    }
}

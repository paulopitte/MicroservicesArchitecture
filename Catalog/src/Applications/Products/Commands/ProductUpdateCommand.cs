 using Catalog.Api.Applications.Products.Validations;
using FluentValidation.Results;
 
namespace Catalog.Api.Applications.Products.Commands
{
    public class ProductUpdateCommand : ProductCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new ProductUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

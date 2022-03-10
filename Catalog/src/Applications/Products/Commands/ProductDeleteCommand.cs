 using Catalog.Api.Applications.Products.Validations;
using FluentValidation.Results;
 
namespace Catalog.Api.Applications.Products.Commands
{
    public class ProductDeleteCommand : ProductCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new ProductDeleteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

 using Catalog.Api.Applications.Products.Validations;
using FluentValidation.Results;
 
namespace Catalog.Api.Applications.Products.Commands
{
    public class ProductCreateCommand : ProductCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new ProductCreateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

using FluentValidation;

namespace Catalog.Api.Applications.Products.Validations
{
    using Commands;

    /// <summary>
    /// Contém as regras para validar um objeto do tipo <see cref="ProductCommand"/>.
    /// </summary>
    public abstract class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
    {

        protected void ValidationId()
        {
            RuleFor(c => c.Id)
                   .NotEmpty()
                   .WithMessage("O Id é obrigátorio!")
                   .WithErrorCode("Product.Id.Empty");
        }

        protected void ValidationSku()
        {
            RuleFor(c => c.Sku)
                   .NotEmpty()
                   .WithMessage("O Sku é obrigátorio!")
                   .WithErrorCode("Product.Sku.Empty")
                   .Length(3, 50)
                   .WithMessage("Sku deve ter entre 36 e 50 caracteres")
                   .WithErrorCode("Product.Sku.BetterThanMaximun");
        }

        protected void ValidationTitle()
        {
            RuleFor(c => c.Title)
                   .NotEmpty()
                   .WithMessage("O Título é obrigátorio!")
                   .WithErrorCode("Product.Title.Empty")
                   .Length(6, 255)
                   .WithMessage("Título deve ter entre 6 e 255 caracteres")
                   .WithErrorCode("Product.Title.BetterThanMaximun");
        }

    }
}

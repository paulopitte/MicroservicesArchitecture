using Core.Common.Messaging;
using FluentValidation.Results;
using MediatR;

namespace Catalog.Api.Core.Application.Products.Handlers
{
    public class ValidateCommandHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : ValidationResult
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is Command command)
                if (!command.IsValid())
                {
                    ValidationResult validations = new();
                    foreach (var erro in command.ValidationResult.Errors)
                        validations.Errors.Add(erro);

                    var response = validations as TResponse;
                    return response;
                }

            TResponse result = await next();
            return result;
        }
 
    }
}

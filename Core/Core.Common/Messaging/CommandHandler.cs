using FluentValidation.Results;
//using Common.Repository;
using System.Threading.Tasks;

namespace Core.Common.Messaging
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler() =>
            ValidationResult = new ValidationResult();


        protected void AddError(string mensagem) =>
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));


        //protected async Task<ValidationResult> Commit(IUnitOfWork uow, string message)
        //{
        //    if (!await uow.Commit()) AddError(message);

        //    return ValidationResult;
        //}

        //protected async Task<ValidationResult> Commit(IUnitOfWork uow) => 
        //    await Commit(uow, "Ops! Ocorreu um erro ao tentar persistir as informações no Banco.").ConfigureAwait(false);

    }
}

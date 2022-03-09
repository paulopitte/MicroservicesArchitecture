using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.Api.Controllers
{
    public class BaseController : ControllerBase
    {

        /// <summary>
        /// Obtém o identificador do canal que encontra-se autenticado.
        /// </summary>
        /// <returns>Identificador do canal.</returns>
        protected int GetChannelId()
        {
            return 999;// Channel.Id;
        }

        /// <summary>
        /// Retorna o identificador de transação ao qual a requisição HTTP atual pertence.
        /// </summary>
        /// <returns>Identificador da transação.</returns>
        protected string GetTransactionId()
        {
            return HttpContext?.Request?.Headers[CatalogHttpHeaders.TransactionId];
        }

        /// <summary>
        /// Retorna o identificador do lote de operações ao qual a requisição HTTP atual pertence.
        /// </summary>
        /// <returns></returns>
        protected string GetBatchOperationId()
        {
            return HttpContext?.Request?.Headers[CatalogHttpHeaders.BatchOperationId];
        }

        /// <summary>
        /// Retorna um dicionario com a estrutura básica para formar o cabeçalho das mensagens do BUS.
        /// </summary>
        /// <returns>Dicionário com chaves de cabeçalho.</returns>
        protected Dictionary<string, string> CreateHeaderDefault()
        {
            return new Dictionary<string, string>
            {
                {
                    "X-Sigc-Header",
                    HttpContext?.Request?.Headers[CatalogHttpHeaders.TransactionId]
                },
                {SettingsHelper.BatchOperationIdKey, GetBatchOperationId()},
                {SettingsHelper.TransactionIdKey, GetTransactionId()},
            };
        }




        private readonly ICollection<string> _errors = new List<string>();



        /// <summary>
        /// Definimos um padrão de resposta para o cliente consumidor
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult JsonResult(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }


            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {

                { "Messages", _errors.ToArray() }
            }));
        }

        protected ActionResult JsonResult(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
                AddError(error.ErrorMessage);

            return JsonResult();
        }

        protected ActionResult JsonResult(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                AddError(error.ErrorMessage);

            return JsonResult();
        }


        protected bool IsOperationValid() => !_errors.Any();
        protected void AddError(string erro) => _errors.Add(erro);
        protected void ClearErrors() => _errors.Clear();

    }
}

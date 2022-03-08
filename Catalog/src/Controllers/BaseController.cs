using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.Api.Controllers
{
    public class BaseController : ControllerBase
    {

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

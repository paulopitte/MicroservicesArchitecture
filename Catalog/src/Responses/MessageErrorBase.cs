using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.Api.Responses
{
    public class MessageErrorBase
    {
        private const string DefaultMessage = "Problema encontrado.";
        private const string ValidationMessage = "Falha na validação.";

        public string Message { get; }
        public IEnumerable<string> Causes { get; }

        public MessageErrorBase() => Message = DefaultMessage;

        public MessageErrorBase(string message) => Message = message;
        public MessageErrorBase(ModelStateDictionary modelState)
        {
            Message = ValidationMessage;
            Causes = modelState
                .Keys
                .SelectMany(key => modelState[key]
                    .Errors
                    .Select(x => GetValidationCause(key, x.ErrorMessage)));
        }
        private static string GetValidationCause(string propertyName, string message) =>
            string.IsNullOrEmpty(propertyName)
                ? message
                : $"{propertyName}: {message}";
    }


}

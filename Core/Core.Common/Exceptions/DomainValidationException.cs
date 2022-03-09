
using System.Collections.Generic;

namespace Core.Common.Exceptions
{
	using Models;
    /// <summary>
    /// Exception utilizada quando ocorre erro no domínio de negócio.
    /// </summary>
    public class DomainValidationException : System.Exception
	{
		/// <summary>
		/// Erros indentificados na validação.
		/// </summary>
		public IEnumerable<MessageFieldError> Errors { get; private set; }

		/// <summary>
		/// Instancia um <see cref="DomainValidationException"/>.
		/// </summary>
		/// <param name="propertyName">Propriedade que foi identificada o erro.</param>
		/// <param name="message">Messagem com detalhes do erro.</param>
		/// <param name="errorCode">Código do erro.</param>
		public DomainValidationException(string propertyName, string message, string errorCode = null)
		{
			Errors = new List<MessageFieldError> { new MessageFieldError { PropertyName = propertyName, Message = message, ErrorCode = errorCode } };
		}

		/// <summary>
		/// Intancia un <see cref="DomainValidationException"/>.
		/// </summary>
		/// <param name="errors">Erros identificados na validação.</param>
		public DomainValidationException(IEnumerable<MessageFieldError> errors)
		{
			Errors = errors;
		}
	}
}

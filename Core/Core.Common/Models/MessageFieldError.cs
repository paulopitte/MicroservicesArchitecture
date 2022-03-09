namespace Core.Common.Models
{
    /// <summary>
    /// Representa a estrura de mensagem de erro para validação de propriedades.
    /// </summary>
    public class MessageFieldError : MessageErrorBase
    {
        /// <summary>
        /// Propriedade que originou o erro.
        /// </summary>
        public string PropertyName { get; set; }
    }
}

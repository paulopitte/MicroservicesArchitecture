namespace Core.Common.Models
{
    /// <summary>
    /// Representa a estrura de mensagem de erro.
    /// </summary>
    public class MessageErrorBase
    {
        /// <summary>
        /// Código de erro.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Mensagem de erro.
        /// </summary>
        public string Message { get; set; }
    }
}

namespace Catalog.Api
{
    /// <summary>
    /// Lista das Chaves de Cabeçalho HTTP conhecidas pela plataforma.
    /// </summary>
    public class CatalogHttpHeaders
    { /// <summary>
      /// Nome da chave de cabeçalho com o identificador único da transação a que uma requisição e/ou resposta HTTP pertence.
      /// </summary>
        public const string TransactionId = "X-Catalog-Transaction-Id";

        /// <summary>
        /// Nome da chave de cabeçalho com o identificador único do lote de operações a que uma requisição e/ou resposta HTTP percente.
        /// </summary>
        public const string BatchOperationId = "X-Batch-Operation-Id";
    }


    internal sealed class SettingsHelper
    {
        /// <summary>
        /// Representa key para identificar o usuário autenticado.
        /// </summary>
        public const string AuthKey = "Authorization";

        /// <summary>
        /// Representa key para acessar as informações do canal autenticado.
        /// </summary>
        public const string ChannelKey = "Catalog-channel";

        /// <summary>
        /// Nome da chave de cabeçalho das mensagens do BUS que identificam a transação a que uma mensagem pertence.
        /// </summary>
        public const string TransactionIdKey = "Catalog-transactionid";

        /// <summary>
        /// Nome da chave de cabeçalho das mensagens do BUS que identificam o lote de operações a que uma mensagem pertence.
        /// </summary>
        public const string BatchOperationIdKey = "Catalog-batchid";
    }
}

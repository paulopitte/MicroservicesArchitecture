using Core.Common.Messaging;

namespace Catalog.Api.Applications.Products.Commands
{
    public abstract class ProductCommand : Command
    {

        /// <summary>
        /// Identificador do produto no catalog.
        /// </summary>
        public string Id { get; set; }   


        /// <summary>
        /// SKU do produto no seller.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Título do produto.
        /// </summary>
        public string Title { get; set; }
        public decimal? Price { get; internal set; }
        public int? Stock { get; internal set; }
    }
}

using Catalog.Api.Domain.Base;

namespace Catalog.Api.Domain
{
    public class Product : BaseEntity
    {

        public Product( string sku,string title, decimal? price = 0M)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Sku = sku;
            this.Title = title;
            this.Price = price;
            Actived();
        }





        /// <summary>
		/// SKU do produto no seller.
		/// </summary>
        public string Sku { get; private set; }

        /// <summary>
        /// Título do produto.
        /// </summary>
        public string Title { get; private set; }
        
        /// <summary>
        /// Representa um preço do produto
        /// </summary>
        public decimal? Price { get;  }

        ///// <summary>
        ///// Descrição do produto.
        ///// </summary>
        //public string Description { get; private set; }

        ///// <summary>
        ///// Descrição abreviada do produto.
        ///// </summary>
        //public string ShortDescription { get; private set; }




        /// <summary>
		/// Quantidade do produto em estoque.
		/// </summary>
		public int? Stock { get; private set; }

        /// <summary>
        /// Quantidade mínima do estoque do produto para possibilitar venda.
        /// </summary>
        public int? StockMinimum { get; private set; }

        /// <summary>
        /// Quantidade do produto disponível através de estoque externo.
        /// </summary>
        public int? StockExternal { get; private set; }





        public string Category { get; set; }





        /// <summary>
        /// Ativar
        /// Marque a caixa para publicar este produto (visível na loja e estoque).
        /// Desmarque para false para não publicar (produto não disponível na loja e estoque).
        /// </summary>
        public override void Actived() => this.Active = true;

        /// <summary>
        /// Desativar
        /// Marque a caixa para publicar este produto (visível na loja e estoque).
        /// Desmarque para false para não publicar (produto não disponível na loja e estoque).
        /// </summary>
        public override void Deactived() => this.Active = false;

        public override void Delete()
        {
            this.Deleted = true;
            this.Active = false;
            this.DeleteAt = DateTime.UtcNow;
        }
    }
}

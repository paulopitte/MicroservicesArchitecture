using Catalog.Api.Domain.Base;

namespace Catalog.Api.Domain
{
    public sealed class Product : BaseEntity
    {
        private const int UTC = -3;

        private Product() { }
        
        public Product(string sku, string title, decimal? price = 0M, int? stock = 0)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Sku = sku;
            this.Title = title;
            this.Price = price;
            this.CreatedAt = DateTime.UtcNow.AddDays(UTC);
            this.Category = "SEM-CATAGORIA";
            this.Active = true;
        }

        /// <summary>
        /// Inclui uma classe aninhada statica personalizando nossa atualização exclusiva de execução á um produto.
        /// </summary>
        public static class Factory
        {
            public static Product Update(string id, string sku, string title, decimal? price = 0M, int? stock = 0)
            {
                return new Product
                {
                    Id = id,
                    Sku = sku,
                    Title = title,
                    Price = price.GetValueOrDefault(),
                    Stock = stock.GetValueOrDefault(),
                    ModifiedAt = DateTime.UtcNow.AddDays(UTC)

                };
            }
        };




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
        public decimal? Price { get; private set; }

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

        ///// <summary>
        ///// Quantidade mínima do estoque do produto para possibilitar venda.
        ///// </summary>
        //public int? StockMinimum { get; private set; }

        ///// <summary>
        ///// Quantidade do produto disponível através de estoque externo.
        ///// </summary>
        //public int? StockExternal { get; private set; }





        public string Category { get; private set; }





        /// <summary>
        /// Ativar
        /// </summary>
        public override void Actived()
        {
            this.Active = true;
            this.ModifiedAt = DateTime.UtcNow.AddDays(UTC);
        }


        /// <summary>
        /// Desativar
        /// </summary>
        public override void Deactived()
        {
            this.Active = false;
            this.ModifiedAt = DateTime.UtcNow.AddDays(UTC);
        }

        public override void Delete()
        {
            this.Deleted = true;
            this.Active = false;
            this.DeleteAt = DateTime.UtcNow.AddDays(UTC);
        }
    }
}

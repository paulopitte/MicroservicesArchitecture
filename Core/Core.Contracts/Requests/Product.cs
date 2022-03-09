namespace Core.Contracts.Requests
{
    public class Product
    {


        /// <summary>
		/// SKU do produto  
		/// </summary>
        public string? Sku { get;  set; }

        /// <summary>
        /// Título do produto.
        /// </summary>
        public string? Title { get;  set; }

        /// <summary>
        /// Representa um preço do produto
        /// </summary>
        public decimal? Price { get; set; } = 0.0M;


        /// <summary>
        /// Representa a quantidade em estoque
        /// </summary>
        public int? Stock { get; set; } = 0;

        /// <summary>
        /// Categoria
        /// </summary>
        public string Category { get; set; }


    }
}

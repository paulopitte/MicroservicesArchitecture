namespace Basket.Api.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; internal set; }
        public decimal Price { get; internal set; }

        public string? ProductId { get; set; }

        public string? ProductName { get; set; }
    }
}
namespace Basket.Api.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = decimal.Zero;

        public string? ProductId { get; set; }

        public string? ProductName { get; set; }
    }
}
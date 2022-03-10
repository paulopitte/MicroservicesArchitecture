namespace Basket.Api.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; internal set; }
        public decimal Price { get; internal set; }
    }
}
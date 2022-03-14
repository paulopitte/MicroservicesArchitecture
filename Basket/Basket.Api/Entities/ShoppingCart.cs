namespace Basket.Api.Entities
{
    public class ShoppingCart
    {

        public ShoppingCart() { }


        public ShoppingCart(string name) =>
            UserName = name;

        public string? UserName { get; set; } = null;

        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }

        //public decimal TotalPrice
        //{
        //    get
        //    {
        //        decimal totalprice = 0;
        //        foreach (var item in Items)
        //            totalprice += item.Price * item.Quantity;

        //        return TotalPrice;
        //    }
        //    set => TotalPrice = value;
        //}


    }
}

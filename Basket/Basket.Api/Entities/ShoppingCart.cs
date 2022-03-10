namespace Basket.Api.Entities
{
    public struct ShoppingCart
    {

        public ShoppingCart()
        {

        }


        public ShoppingCart(string name) =>
            UserName = name;

        public string UserName { get; set; }


        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();


        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                    totalprice += item.Price * item.Quantity;

                return TotalPrice;
            }
        }



    }
}

namespace Basket.Api.Entities
{
    public struct BasketCheckout
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        //Billing Address
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        //Payment
        public int PaymentMethod { get; set; }

    }
}

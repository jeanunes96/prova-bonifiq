namespace ProvaPub.Dtos
{
    public class PlaceOrderRequest
    {
        public string PaymentMethod { get; set; }
        public decimal PaymentValue { get; set; }
        public int CustomerId { get; set; }
    }
}

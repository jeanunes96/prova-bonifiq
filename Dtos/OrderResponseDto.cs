namespace ProvaPub.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal PaymentValue { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

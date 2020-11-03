namespace WebApi.Models
{
    public class TransactionRequest
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }
}
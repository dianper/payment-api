namespace Application.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentGetResult
    {
        public Guid MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
        public decimal Amount { get; set; }
        public string CardNumberMasked { get; set; }
    }
}
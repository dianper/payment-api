namespace Application.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentGetResult
    {
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string CardNumberMasked { get; set; }
        public Guid? TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
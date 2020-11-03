namespace Repository.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Repository.Enums;

    [ExcludeFromCodeCoverage]
    public class Payment : BaseEntity
    {
        public Guid MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime Created { get; set; }
        public string TransactionStatus { get; set; }
    }
}

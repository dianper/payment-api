namespace Application.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentGetRequest
    {
        public Guid MerchantId { get; set; }
        public Guid? PaymentId { get; set; }
    }
}
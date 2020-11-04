namespace Application.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentPostResult
    {
        public PaymentPostResult(Guid paymentId, Guid? transactionId, string transactionStatus)
        {
            this.PaymentId = paymentId;
            this.TransactionId = transactionId;
            this.TransactionStatus = transactionStatus;
        }

        public Guid PaymentId { get; }
        public Guid? TransactionId { get; }
        public string TransactionStatus { get; }
    }
}
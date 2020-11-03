namespace Application.Models
{
    using System;

    public class PaymentPostResult
    {
        public PaymentPostResult(Guid paymentId = default, string transactionStatus = default)
        {
            this.PaymentId = paymentId;
            this.TransactionStatus = transactionStatus;
        }

        public Guid PaymentId { get; }
        public string TransactionStatus { get; }
    }
}
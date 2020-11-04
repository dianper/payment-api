namespace WebApi.Models
{
    using System;

    public class TransactionResult
    {
        public TransactionResult()
        {
            this.TransactionId = Guid.NewGuid();
        }

        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
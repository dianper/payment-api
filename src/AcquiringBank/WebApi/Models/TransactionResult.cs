namespace WebApi.Models
{
    using System;

    public class TransactionResult
    {
        public TransactionResult()
        {
            this.TransactionId = new Guid();
        }

        public Guid TransactionId { get; }
        public string TransactionStatus { get; set; }
    }
}
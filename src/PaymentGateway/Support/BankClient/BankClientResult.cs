namespace Support.BankClient
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BankClientResult
    {
        public BankClientResult(Guid transactionId, string transactionStatus)
        {
            this.TransactionId = transactionId;
            this.TransactionStatus = transactionStatus;
        }

        public Guid TransactionId { get; }
        public string TransactionStatus { get; }
    }
}
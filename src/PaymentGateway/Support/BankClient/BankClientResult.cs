namespace Support.BankClient
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BankClientResult
    {
        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
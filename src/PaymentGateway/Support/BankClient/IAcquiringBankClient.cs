namespace Support.BankClient
{
    using System.Threading.Tasks;

    public interface IAcquiringBankClient
    {
        Task<BankClientResult> ProcessTransactionAsync(BankClientRequest bankClientRequest);
    }
}
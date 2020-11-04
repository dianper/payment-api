namespace Support.BankClient
{
    using System.Threading.Tasks;

    public interface IAcquiringBankClient
    {
        Task<BankClientResult> ProcessTransaction(BankClientRequest bankClientRequest);
    }
}
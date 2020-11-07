namespace Repository.DataAccess
{
    using System.Diagnostics.CodeAnalysis;
    using Repository.Interfaces;
    using Repository.Models;

    [ExcludeFromCodeCoverage]
    public class MerchantRepository : BaseRepository<Merchant>, IMerchantRepository
    {
        public MerchantRepository(PaymentContext context) : base(context) { }
    }
}
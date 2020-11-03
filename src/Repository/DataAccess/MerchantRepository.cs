namespace Repository.DataAccess
{
    using System;
    using System.Threading.Tasks;
    using Repository.Interfaces;
    using Repository.Models;

    public class MerchantRepository : BaseRepository<Merchant>, IMerchantRepository
    {
        public MerchantRepository(PaymentContext context) : base(context) { }

        public async Task<bool> Exists(Guid id)
        {
            var merchant = await this.GetByIdAsync(id);

            return merchant != null;
        }
    }
}
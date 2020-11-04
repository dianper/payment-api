namespace Repository.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repository.Interfaces;
    using Repository.Models;

    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext context) : base(context) { }

        public async Task<IEnumerable<Payment>> GetPaymentsDetailsAsync(Guid merchantId, Guid? paymentId)
        {
            var payments = await this.GetAllByExpressionAsync(x => x.MerchantId == merchantId);

            if (paymentId.HasValue)
            {
                payments = payments.Where(p => p.Id == paymentId);
            }

            return payments;
        }
    }
}
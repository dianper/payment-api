namespace Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Repository.Models;

    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsDetailsAsync(Guid merchantId, Guid? paymentId);
    }
}
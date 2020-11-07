namespace Repository.DataAccess
{
    using System.Diagnostics.CodeAnalysis;
    using Repository.Interfaces;
    using Repository.Models;

    [ExcludeFromCodeCoverage]
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext context) : base(context) { }
    }
}
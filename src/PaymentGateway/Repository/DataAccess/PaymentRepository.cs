namespace Repository.DataAccess
{
    using Repository.Interfaces;
    using Repository.Models;

    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext context) : base(context) { }
    }
}
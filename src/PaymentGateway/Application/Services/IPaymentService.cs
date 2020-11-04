namespace Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Models;

    public interface IPaymentService
    {
        Task<BaseResult<PaymentPostResult>> ProcessPaymentAsync(PaymentPostRequest paymentPostRequest);
        Task<BaseResult<IEnumerable<PaymentGetResult>>> RetrievePaymentsDetailsAsync(PaymentGetRequest paymentGetRequest);
    }
}
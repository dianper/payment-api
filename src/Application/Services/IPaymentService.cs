namespace Application.Services
{
    using System.Threading.Tasks;
    using Application.Models;

    public interface IPaymentService
    {
        Task<BaseResult<PaymentPostResult>> ProcessPaymentAsync(PaymentPostRequest paymentPostRequest);
        Task<BaseResult<PaymentGetResult>> RetrievePaymentDetailsAsync(PaymentGetRequest paymentGetRequest);
    }
}
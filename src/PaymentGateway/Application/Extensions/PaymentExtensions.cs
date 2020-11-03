namespace Application.Extensions
{
    using Application.Models;
    using Repository.Models;

    public static class PaymentExtensions
    {
        public static PaymentPostResult ToPostResult(this Payment payment)
        {
            if (payment == null)
            {
                return default;
            }

            return new PaymentPostResult(payment.Id, payment.TransactionStatus);
        }

        public static PaymentGetResult ToGetResult(this Payment payment)
        {
            if (payment == null)
            {
                return default;
            }

            return new PaymentGetResult
            {
                Amount = payment.Amount,
                CardNumberMasked = payment.CardNumber.ToMask(),
                MerchantId = payment.MerchantId,
                PaymentId = payment.Id,
                TransactionDate = payment.Created,
                TransactionStatus = payment.TransactionStatus
            };
        }
    }
}
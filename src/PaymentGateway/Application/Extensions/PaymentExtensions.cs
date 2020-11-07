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

            return new PaymentPostResult(payment.Id, payment.TransactionId, payment.TransactionStatus);
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
                CardNumberMasked = payment.CardNumber.ToCardMasked(),
                PaymentId = payment.Id,
                PaymentDate = payment.Created,
                TransactionId = payment.TransactionId,
                TransactionStatus = payment.TransactionStatus
            };
        }
    }
}
namespace Application.Extensions
{
    using Application.Models;
    using Repository.Models;
    using System.Collections.Generic;
    using System.Linq;

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

        public static IEnumerable<PaymentGetResult> ToDetailsResult(this IEnumerable<Payment> payments)
        {
            if (!payments.Any())
            {
                return default;
            }

            var result = new List<PaymentGetResult>();

            foreach (var payment in payments)
            {
                result.Add(payment.ToGetResult());
            }

            return result;
        }

        private static PaymentGetResult ToGetResult(this Payment payment) =>
            new PaymentGetResult
            {
                Amount = payment.Amount,
                CardNumberMasked = payment.CardNumber.ToMask(),
                PaymentId = payment.Id,
                PaymentDate = payment.Created,
                TransactionId = payment.TransactionId,
                TransactionStatus = payment.TransactionStatus
            };
    }
}
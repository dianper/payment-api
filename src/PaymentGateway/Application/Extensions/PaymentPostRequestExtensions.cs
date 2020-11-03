namespace Application.Extensions
{
    using System;
    using Application.Models;
    using Repository.Enums;
    using Repository.Models;

    public static class PaymentPostRequestExtensions
    {
        public static Payment ToPaymentModel(this PaymentPostRequest paymentPostRequest, Currency currency, string transactionStatus) =>
            new Payment
            {
                Amount = paymentPostRequest.Amount,
                CardNumber = paymentPostRequest.CardNumber,
                Created = DateTime.Now,
                Currency = currency,
                ExpiryMonth = paymentPostRequest.ExpiryMonth,
                ExpiryYear = paymentPostRequest.ExpiryYear,
                MerchantId = paymentPostRequest.MerchantId,
                SecurityCode = paymentPostRequest.SecurityCode,
                TransactionStatus = transactionStatus
            };
    }
}
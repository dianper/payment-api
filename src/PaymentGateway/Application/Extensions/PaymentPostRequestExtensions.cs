namespace Application.Extensions
{
    using System;
    using Application.Models;
    using Repository.Enums;
    using Repository.Models;
    using Support.BankClient;

    public static class PaymentPostRequestExtensions
    {
        public static Payment ToPaymentModel(this PaymentPostRequest paymentPostRequest, Currency currency = Currency.USD, BankClientResult bankClientResult = default) =>
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
                TransactionId = bankClientResult?.TransactionId,
                TransactionStatus = bankClientResult?.TransactionStatus
            };

        public static BankClientRequest ToBankClientRequest(this PaymentPostRequest paymentPostRequest) =>
            new BankClientRequest
            {
                Amount = paymentPostRequest.Amount,
                CardNumber = paymentPostRequest.CardNumber,
                ExpiryMonth = paymentPostRequest.ExpiryMonth,
                ExpiryYear = paymentPostRequest.ExpiryYear,
                SecurityCode = paymentPostRequest.SecurityCode
            };
    }
}
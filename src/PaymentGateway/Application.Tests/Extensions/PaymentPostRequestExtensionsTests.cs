namespace Application.Tests.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Application.Extensions;
    using Application.Models;
    using Repository.Enums;
    using Repository.Models;
    using Support.BankClient;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class PaymentPostRequestExtensionsTests
    {
        [Fact]
        public void ToPaymentModel_HasNoCurrencyAndHasNoBankClientResult_ReturnsModel()
        {
            // Arrange
            var paymentPostRequest = new PaymentPostRequest
            {
                Amount = 100,
                CardNumber = "1234567890123456",
                Currency = "USD",
                ExpiryMonth = 12,
                ExpiryYear = 2020,
                MerchantId = Guid.NewGuid(),
                SecurityCode = 123
            };

            // Act
            var result = paymentPostRequest.ToPaymentModel();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.TransactionId);
            Assert.IsType<Payment>(result);
        }

        [Fact]
        public void ToPaymentModel_HappyJourney_ReturnsModel()
        {
            // Arrange
            var paymentPostRequest = new PaymentPostRequest
            {
                Amount = 100,
                CardNumber = "1234567890123456",
                Currency = "USD",
                ExpiryMonth = 12,
                ExpiryYear = 2020,
                MerchantId = Guid.NewGuid(),
                SecurityCode = 123
            };

            var bankClientResult = new BankClientResult
            {
                TransactionId = Guid.NewGuid(),
                TransactionStatus = "ok"
            };

            // Act
            var result = paymentPostRequest.ToPaymentModel(Currency.EUR, bankClientResult);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TransactionId);
            Assert.IsType<Payment>(result);
        }

        [Fact]
        public void ToBankClientRequest_ReturnsModel()
        {
            // Arrange
            var paymentPostRequest = new PaymentPostRequest
            {
                Amount = 100,
                CardNumber = "1234567890123456",
                ExpiryMonth = 12,
                ExpiryYear = 2020,
                SecurityCode = 123
            };

            // Act
            var result = paymentPostRequest.ToBankClientRequest();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BankClientRequest>(result);
        }
    }
}
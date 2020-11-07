namespace Application.Tests.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Application.Extensions;
    using Repository.Models;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class PaymentExtensionsTests
    {
        [Fact]
        public void ToPostResult_WhenHasNoPayment_ReturnsDefault()
        {
            // Arrange
            var payment = default(Payment);

            // Act & Assert
            Assert.Null(payment.ToPostResult());
        }

        [Fact]
        public void ToPostResult_HappyJourney_ReturnsResult()
        {
            // Arrange
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                TransactionId = Guid.NewGuid(),
                TransactionStatus = "ok"
            };

            // Act & Assert
            Assert.NotNull(payment.ToPostResult());
        }

        [Fact]
        public void ToGetResult_WhenHasNoPayment_ReturnsDefault()
        {
            // Arrange
            var payment = default(Payment);

            // Act & Assert
            Assert.Null(payment.ToGetResult());
        }

        [Fact]
        public void ToGetResult_HappyJourney_ReturnsResult()
        {
            // Arrange
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                TransactionId = Guid.NewGuid(),
                TransactionStatus = "ok"
            };

            // Act & Assert
            Assert.NotNull(payment.ToGetResult());
        }
    }
}
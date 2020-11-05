namespace Application.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
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

            // Act
            var result = payment.ToPostResult();

            // Assert
            Assert.Null(result);
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

            // Act
            var result = payment.ToPostResult();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ToDetailsResult_WhenNullPayments_ReturnsDefault()
        {
            // Arrange
            var payments = default(IEnumerable<Payment>);

            // Act & Assert
            Assert.Null(payments.ToDetailsResult());
        }

        [Fact]
        public void ToDetailsResult_WhenEmptyPayments_ReturnsDefault()
        {
            // Arrange
            var payments = Enumerable.Empty<Payment>();

            // Act & Assert
            Assert.Null(payments.ToDetailsResult());
        }

        [Fact]
        public void ToDetailsResult_WhenHasPayments_ReturnsResult()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new Payment
                {
                    Id = Guid.NewGuid(),
                    Amount = 100,
                    CardNumber = "1234567890123456",
                    Created = DateTime.Now,
                    TransactionId = Guid.NewGuid(),
                    TransactionStatus = "OK"
                }
            };

            // Act
            var details = payments.ToDetailsResult();

            // Assert
            Assert.NotNull(details);
            Assert.NotEmpty(details);
        }
    }
}
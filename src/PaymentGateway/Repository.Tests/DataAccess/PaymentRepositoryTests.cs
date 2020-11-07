namespace Repository.Tests.DataAccess
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Repository.DataAccess;
    using Repository.Models;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class PaymentRepositoryTests
    {
        private readonly PaymentContext paymentContext;
        private readonly PaymentRepository target;

        public PaymentRepositoryTests()
        {
            this.paymentContext = new DbContextFactory().GetPaymentContext();
            this.target = new PaymentRepository(this.paymentContext);
        }

        [Fact]
        public async Task GetPaymentsDetailsAsync_HasNoPaymentId_ReturnsPayments()
        {
            // Arrange
            var merchantId = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3");
            this.paymentContext.Add(new Payment { MerchantId = merchantId });
            this.paymentContext.SaveChanges();

            // Act
            var result = await this.target.GetPaymentsDetailsAsync(merchantId, default);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetPaymentsDetailsAsync_HasPaymentId_ReturnsPayments()
        {
            // Arrange
            var merchantId = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3");
            var paymentId = new Guid("3e57e197-ed6c-4c85-8d14-e7185e2b9a3d");

            this.paymentContext.Add(new Payment { Id = paymentId, MerchantId = merchantId });
            this.paymentContext.SaveChanges();

            // Act
            var result = await this.target.GetPaymentsDetailsAsync(merchantId, paymentId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetPaymentsDetailsAsync_HasPaymentIdButNotMatch_ReturnsEmpty()
        {
            // Arrange
            var merchantId = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3");
            var paymentId = new Guid("3e57e197-ed6c-4c85-8d14-e7185e2b9a3d");

            this.paymentContext.Add(new Payment { Id = Guid.NewGuid(), MerchantId = merchantId });
            this.paymentContext.SaveChanges();

            // Act
            var result = await this.target.GetPaymentsDetailsAsync(merchantId, paymentId);

            // Assert
            Assert.Empty(result);
        }
    }
}
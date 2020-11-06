namespace Repository.Tests.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Repository.DataAccess;
    using Repository.Models;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class PaymentRepositoryTests
    {
        private readonly PaymentRepository target;

        public PaymentRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseInMemoryDatabase("fakedb");

            var context = new PaymentContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Add(new Payment
            {
                Id = new Guid("3e57e197-ed6c-4c85-8d14-e7185e2b9a3d"),
                MerchantId = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3")
            });
            context.SaveChanges();

            this.target = new PaymentRepository(context);
        }

        [Fact]
        public async Task GetPaymentsDetailsAsync_HasNoPaymentId_ReturnsPayments()
        {
            // Arrange
            var merchantId = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3");

            // Act
            var result = await this.target.GetPaymentsDetailsAsync(merchantId, default);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetPaymentsDetailsAsync_HasPaymentId_ReturnsPayments()
        {
            // Arrange
            var merchantId = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3");
            var paymentId = new Guid("3e57e197-ed6c-4c85-8d14-e7185e2b9a3d");

            // Act
            var result = await this.target.GetPaymentsDetailsAsync(merchantId, paymentId);

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
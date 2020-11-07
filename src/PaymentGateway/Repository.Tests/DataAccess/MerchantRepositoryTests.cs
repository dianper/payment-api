namespace Repository.Tests.DataAccess
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Repository.DataAccess;
    using Repository.Models;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class MerchantRepositoryTests
    {
        private readonly PaymentContext paymentContext;
        private readonly MerchantRepository target;

        public MerchantRepositoryTests()
        {
            this.paymentContext = new DbContextFactory().GetPaymentContext();
            this.target = new MerchantRepository(this.paymentContext);
        }

        [Fact]
        public async Task Exists_ReturnsTrue()
        {
            // Arrange
            var merchandId = new Guid("5373afd5-ad64-4bfd-9121-14b3a17883e8");
            this.paymentContext.Add(new Merchant { Id = merchandId });
            this.paymentContext.SaveChanges();

            // Act & Assert
            Assert.True(await this.target.Exists(merchandId));
        }

        [Fact]
        public async Task Exists_ReturnsFalse()
        {
            // Act & Assert
            Assert.False(await this.target.Exists(Guid.NewGuid()));
        }
    }
}
namespace Repository.Tests.DataAccess
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.DataAccess;
    using Repository.Models;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class MerchantRepositoryTests
    {
        private readonly MerchantRepository target;

        public MerchantRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseInMemoryDatabase("fakedb");

            var context = new PaymentContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Add(new Merchant { Id = new Guid("5373afd5-ad64-4bfd-9121-14b3a17883e8"), Name = "name" });
            context.SaveChanges();

            this.target = new MerchantRepository(context);
        }

        [Fact]
        public async Task Exists_ReturnsTrue()
        {
            // Act & Assert
            Assert.True(await this.target.Exists(new Guid("5373afd5-ad64-4bfd-9121-14b3a17883e8")));
        }

        [Fact]
        public async Task Exists_ReturnsFalse()
        {
            // Act & Assert
            Assert.False(await this.target.Exists(Guid.NewGuid()));
        }
    }
}
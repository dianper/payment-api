namespace Repository.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.Models;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class BaseRepositoryTests
    {
        private PaymentContext paymentContext;
        private BaseRepository<Merchant> target;

        public BaseRepositoryTests()
        {
            this.SetupPaymentContext();
        }

        [Fact]
        public async Task GetAllByExpressionAsync()
        {
            // Act
            var result = await this.target.GetAllByExpressionAsync(x => x.Name.Equals("name"));

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            var id = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3");

            // Act
            var result = await this.target.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("name", result.Name);
        }

        [Fact]
        public async Task InsertAsync()
        {
            // Act
            var result = await this.target.InsertAsync(new Merchant { Name = "m2" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("m2", result.Name);
        }

        private void SetupPaymentContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseInMemoryDatabase("testdb");

            this.paymentContext = new PaymentContext(optionsBuilder.Options);
            this.paymentContext.Database.EnsureDeleted();
            this.paymentContext.Database.EnsureCreated();
            this.paymentContext.AddRange(new Merchant { Id = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3"), Name = "name" }, new Merchant { Name = "merchant" });
            this.paymentContext.SaveChanges();

            this.target = new BaseRepository<Merchant>(this.paymentContext);
        }
    }
}
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
        private readonly BaseRepository<Merchant> target;

        public BaseRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseInMemoryDatabase("fakedb");

            var context = new PaymentContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.AddRange(this.GetFakeMerchants());
            context.SaveChanges();

            this.target = new BaseRepository<Merchant>(context);
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
            var result = await this.target.InsertAsync(new Merchant { Id = Guid.NewGuid(), Name = "m2" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("m2", result.Name);
        }

        private Merchant[] GetFakeMerchants() =>
            new[]
            {
                new Merchant
                {
                    Id = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3"),
                    Name = "name"
                },
                new Merchant
                {
                    Id = Guid.NewGuid(),
                    Name = "merchant"
                }
            };
    }
}
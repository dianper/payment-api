namespace Repository.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class BaseRepositoryTests
    {
        private readonly FakeDbContext fakeDbContext;
        private readonly BaseRepository<MockEntity> target;

        public BaseRepositoryTests()
        {
            this.fakeDbContext = new DbContextFactory().GetFakeContext();
            this.target = new BaseRepository<MockEntity>(this.fakeDbContext);
        }

        [Fact]
        public async Task GetAllByExpressionAsync()
        {
            // Arrange
            this.fakeDbContext.AddRange(this.GetFakeEntities());
            this.fakeDbContext.SaveChanges();

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
            this.fakeDbContext.AddRange(this.GetFakeEntities());
            this.fakeDbContext.SaveChanges();

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
            var result = await this.target.InsertAsync(new MockEntity { Id = Guid.NewGuid(), Name = "m2" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("m2", result.Name);
        }

        private MockEntity[] GetFakeEntities() =>
            new[]
            {
                new MockEntity
                {
                    Id = new Guid("caab5cc6-01bd-42d4-bb1e-ec17433161b3"),
                    Name = "name"
                },
                new MockEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "merchant"
                }
            };
    }
}
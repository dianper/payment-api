namespace Repository.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;

    [ExcludeFromCodeCoverage]
    public class DbContextFactory
    {
        public FakeDbContext GetFakeContext()
        {
            var context = new FakeDbContext(this.GetOptions("fakedb"));
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        private DbContextOptions<PaymentContext> GetOptions(string dbName) =>
            new DbContextOptionsBuilder<PaymentContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
    }
}
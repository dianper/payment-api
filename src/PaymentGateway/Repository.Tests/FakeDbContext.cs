namespace Repository.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Repository.Models;

    [ExcludeFromCodeCoverage]
    public class FakeDbContext : PaymentContext
    {
        public DbSet<MockEntity> MockEntity { get; set; }

        public FakeDbContext(DbContextOptions<PaymentContext> options) : base(options) { }
    }

    [ExcludeFromCodeCoverage]
    public class MockEntity : BaseEntity
    {
        public string Name { get; set; }
    }
}
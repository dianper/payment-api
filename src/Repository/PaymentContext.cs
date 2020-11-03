namespace Repository
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Repository.Models;

    [ExcludeFromCodeCoverage]
    public class PaymentContext : DbContext
    {
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }
    }
}
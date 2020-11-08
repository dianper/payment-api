namespace Repository
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Repository.Models;

    [ExcludeFromCodeCoverage]
    public static class Seed
    {
        private static readonly DbContextOptionsBuilder<PaymentContext> optionsBuilder;

        static Seed()
        {
            optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseInMemoryDatabase("paymentdb");
        }

        public static void Create()
        {
            using (var context = new PaymentContext(optionsBuilder.Options))
            {
                var merchants = new[]
                {
                    new Merchant
                    {
                        Id = new Guid("63d33faf-7781-48c9-a2f5-a035d1799735"),
                        Name = "Merchant 1"
                    },
                    new Merchant
                    {
                        Id = new Guid("e5f18ed2-3a06-40b1-85db-3eec9624cc0f"),
                        Name = "Merchant 2"
                    }
                };

                context.Merchant.AddRange(merchants);
                context.SaveChanges();
            }
        }
    }
}
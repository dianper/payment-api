namespace WebApi.Dependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    using Repository.DataAccess;
    using Repository.Interfaces;

    [ExcludeFromCodeCoverage]
    public static class RepositoryDependencies
    {
        public static void RegisterRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddDbContext<PaymentContext>(options => options.UseInMemoryDatabase("paymentdb"));
        }
    }
}
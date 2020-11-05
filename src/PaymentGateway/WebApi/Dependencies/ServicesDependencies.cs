namespace WebApi.Dependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Application.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Support.BankClient;

    [ExcludeFromCodeCoverage]
    public static class ServicesDependencies
    {
        public static void RegisterServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAcquiringBankClient, AcquiringBankClient>();
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}
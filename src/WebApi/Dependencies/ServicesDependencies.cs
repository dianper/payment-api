namespace WebApi.Dependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Application.Services;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class ServicesDependencies
    {
        public static void RegisterServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}
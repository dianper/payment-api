namespace WebApi.Dependencies
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;
    using WebApi.Configuration;

    [ExcludeFromCodeCoverage]
    public static class HttpDependencies
    {
        public static void RegisterHttpDependencies(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services
                .AddHttpClient(appConfiguration.BankConfiguration.ServiceName, c =>
                {
                    c.BaseAddress = new Uri(appConfiguration.BankConfiguration.BaseAddress);
                    c.Timeout = TimeSpan.FromMilliseconds(appConfiguration.BankConfiguration.Timeout);
                });
        }
    }
}
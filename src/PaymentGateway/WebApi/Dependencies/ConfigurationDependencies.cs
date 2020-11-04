namespace WebApi.Dependencies
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;
    using WebApi.Configuration;

    [ExcludeFromCodeCoverage]
    public static class ConfigurationDependencies
    {
        public static void RegisterConfigurationDependencies(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services.AddSingleton(appConfiguration.BankConfiguration);
            services.AddSingleton(appConfiguration.SwaggerConfiguration);
        }
    }
}
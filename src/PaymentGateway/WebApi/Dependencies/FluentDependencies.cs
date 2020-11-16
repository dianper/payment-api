namespace WebApi.Dependencies
{
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;
    using WebApi.Validators;

    [ExcludeFromCodeCoverage]
    public static class FluentDependencies
    {
        public static void RegisterFluentDependencies(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<PaymentGetValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<PaymentPostValidator>();
                });
        }
    }
}
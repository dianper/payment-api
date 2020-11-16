namespace WebApi.Dependencies
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using WebApi.Configuration;

    [ExcludeFromCodeCoverage]
    public static class AuthDependencies
    {
        public static void RegisterAuthDependencies(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appConfiguration.AuthConfiguration.Issuer,
                        ValidAudience = appConfiguration.AuthConfiguration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration.AuthConfiguration.Key))
                    };
                });
        }
    }
}
namespace WebApi.Dependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using WebApi.Configuration;

    [ExcludeFromCodeCoverage]
    public static class SwaggerDependencies
    {
        public static void RegisterSwaggerDependencies(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services
                .AddSwaggerGen(sg =>
                {
                    sg.SwaggerDoc(appConfiguration.SwaggerConfiguration.Version, new OpenApiInfo
                    {
                        Version = appConfiguration.SwaggerConfiguration.Version,
                        Title = appConfiguration.SwaggerConfiguration.Title,
                        Description = appConfiguration.SwaggerConfiguration.Description,
                    });

                    sg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Specify the authorization token",
                    });

                    sg.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                 Reference = new OpenApiReference
                                 {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                 }
                            },
                            new string[] { }
                        }
                    });
                });
        }
    }
}
namespace WebApi
{
    using System;
    using System.Text;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Repository;
    using WebApi.Configuration;
    using WebApi.Dependencies;
    using WebApi.Validators;

    public class Startup
    {
        private readonly AppConfiguration appConfiguration;

        public Startup(IConfiguration configuration)
        {
            this.appConfiguration = configuration.Get<AppConfiguration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterConfigurationDependencies(this.appConfiguration);
            services
                .AddHttpClient(this.appConfiguration.BankConfiguration.ServiceName, c =>
                {
                    c.BaseAddress = new Uri(this.appConfiguration.BankConfiguration.BaseAddress);
                    c.Timeout = TimeSpan.FromMilliseconds(this.appConfiguration.BankConfiguration.Timeout);
                });

            services.RegisterRepositoryDependencies();
            services.RegisterServicesDependencies();

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
                        ValidIssuer = this.appConfiguration.AuthConfiguration.Issuer,
                        ValidAudience = this.appConfiguration.AuthConfiguration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appConfiguration.AuthConfiguration.Key))
                    };
                });

            services
                .AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<PaymentGetValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<PaymentPostValidator>();
                });

            services
                .AddSwaggerGen(sg =>
                {
                    sg.SwaggerDoc(this.appConfiguration.SwaggerConfiguration.Version, new OpenApiInfo
                    {
                        Version = this.appConfiguration.SwaggerConfiguration.Version,
                        Title = this.appConfiguration.SwaggerConfiguration.Title,
                        Description = this.appConfiguration.SwaggerConfiguration.Description,
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout Payment Gateway Api v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add merchants
            Seed.Create();
        }
    }
}

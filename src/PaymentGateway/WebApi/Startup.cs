namespace WebApi
{
    using System;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
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
            services.AddHttpClient(this.appConfiguration.BankConfiguration.ServiceName, c =>
            {
                c.BaseAddress = new Uri(this.appConfiguration.BankConfiguration.BaseAddress);
                c.Timeout = TimeSpan.FromMilliseconds(this.appConfiguration.BankConfiguration.Timeout);
            });

            services.RegisterRepositoryDependencies();
            services.RegisterServicesDependencies();

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<PaymentGetValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<PaymentPostValidator>();
            });

            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc(this.appConfiguration.SwaggerConfiguration.Version, new OpenApiInfo
                {
                    Version = this.appConfiguration.SwaggerConfiguration.Version,
                    Title = this.appConfiguration.SwaggerConfiguration.Title,
                    Description = this.appConfiguration.SwaggerConfiguration.Description,
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

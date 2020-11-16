namespace WebApi
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Prometheus;
    using Repository;
    using WebApi.Configuration;
    using WebApi.Dependencies;
    using WebApi.Middlewares;

    [ExcludeFromCodeCoverage]
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
            services.RegisterHttpDependencies(this.appConfiguration);
            services.RegisterRepositoryDependencies();
            services.RegisterServicesDependencies();
            services.RegisterAuthDependencies(this.appConfiguration);
            services.RegisterSwaggerDependencies(this.appConfiguration);
            services.RegisterFluentDependencies();
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

            // Prometheus Metrics
            app.UseHttpMetrics();
            app.UseMetricServer();
            app.UseMiddleware<MetricsMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });

            // Add merchants
            Seed.Create();
        }
    }
}

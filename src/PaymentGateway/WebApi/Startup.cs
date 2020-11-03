namespace WebApi
{
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Repository;
    using WebApi.Dependencies;
    using WebApi.Validators;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterRepositoryDependencies();
            services.RegisterServicesDependencies();

            services.AddControllers().AddFluentValidation(fv => 
            {
                fv.RegisterValidatorsFromAssemblyContaining<PaymentGetValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<PaymentPostValidator>();
            });

            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Checkout Payment Gateway",
                    Description = "Responsible for processing payments",
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

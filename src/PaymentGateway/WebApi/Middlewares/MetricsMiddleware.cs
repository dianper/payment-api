namespace WebApi.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Prometheus;

    public class MetricsMiddleware
    {
        private readonly RequestDelegate request;

        public MetricsMiddleware(RequestDelegate request)
        {
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.Value;
            var method = httpContext.Request.Method;

            var counter = Metrics.CreateCounter("total_requests", "HTTP Requests Total", new CounterConfiguration
            {
                LabelNames = new[] { "path", "method", "status" }
            });

            try
            {
                await this.request.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                counter.Labels(path, method, httpContext.Response.StatusCode.ToString()).Inc();
                throw ex;
            }

            if (path != "/metrics")
            {
                counter.Labels(path, method, httpContext.Response.StatusCode.ToString()).Inc();
            }
        }
    }
}
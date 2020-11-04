namespace WebApi.Configuration
{
    using Support.Configuration;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AppConfiguration
    {
        public BankConfiguration BankConfiguration { get; set; }
        public SwaggerConfiguration SwaggerConfiguration { get; set; }
    }
}
namespace Support.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SwaggerConfiguration
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
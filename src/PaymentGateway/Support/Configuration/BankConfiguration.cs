namespace Support.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BankConfiguration
    {
        public string BaseAddress { get; set; }
        public string Endpoint { get; set; }
        public string ServiceName { get; set; }
        public int Timeout { get; set; }
    }
}
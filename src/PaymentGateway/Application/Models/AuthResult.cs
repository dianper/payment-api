namespace Application.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AuthResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
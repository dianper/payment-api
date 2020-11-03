namespace Repository.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
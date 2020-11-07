namespace Application.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentGetRequest
    {
        [Required]
        public Guid PaymentId { get; set; }
    }
}
namespace WebApi.Validators
{
    using System.Diagnostics.CodeAnalysis;
    using Application.Models;
    using FluentValidation;

    [ExcludeFromCodeCoverage]
    public class PaymentGetValidator : AbstractValidator<PaymentGetRequest>
    {
        public PaymentGetValidator()
        {
            RuleFor(x => x.PaymentId).NotNull().NotEmpty();
        }
    }
}
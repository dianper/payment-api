namespace WebApi.Validators
{
    using Application.Models;
    using FluentValidation;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentGetValidator : AbstractValidator<PaymentGetRequest>
    {
        public PaymentGetValidator()
        {
            RuleFor(x => x.MerchantId).NotNull().NotEmpty();
        }
    }
}
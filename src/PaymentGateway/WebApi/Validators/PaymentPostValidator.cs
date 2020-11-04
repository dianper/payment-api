namespace WebApi.Validators
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Application.Models;
    using FluentValidation;

    [ExcludeFromCodeCoverage]
    public class PaymentPostValidator : AbstractValidator<PaymentPostRequest>
    {
        public PaymentPostValidator()
        {
            RuleFor(x => x.MerchantId).NotNull().NotEmpty();
            RuleFor(x => x.CardNumber).CreditCard();
            RuleFor(x => x.ExpiryMonth).NotNull().GreaterThan(0).LessThanOrEqualTo(12);
            RuleFor(x => x.ExpiryYear).NotNull().GreaterThanOrEqualTo(x => DateTime.Now.Year);
            RuleFor(x => x.SecurityCode).NotNull().LessThanOrEqualTo(999);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Currency).NotNull().Length(3);
        }
    }
}
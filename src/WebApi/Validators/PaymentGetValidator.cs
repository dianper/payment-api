namespace WebApi.Validators
{
    using Application.Models;
    using FluentValidation;

    public class PaymentGetValidator : AbstractValidator<PaymentGetRequest>
    {
        public PaymentGetValidator()
        {
            RuleFor(x => x.PaymentId).NotNull().NotEmpty();
        }
    }
}
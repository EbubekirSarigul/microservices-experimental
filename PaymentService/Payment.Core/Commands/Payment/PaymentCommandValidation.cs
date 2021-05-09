using FluentValidation;
using System;

namespace Payment.Core.Commands.Payment
{
    public class PaymentCommandValidation : AbstractValidator<PaymentCommand>
    {
        public PaymentCommandValidation()
        {
            RuleFor(x => x.PaymentId).NotEmpty().WithErrorCode("EMPTY_PAYMENT_ID").WithMessage("Payment id is required.")
                                     .Must(BeGuid).WithErrorCode("INVALID_PAYMENT_ID").WithMessage("Payment id is invalid.");
        }

        private bool BeGuid(string value)
        {
            if (Guid.TryParse(value, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

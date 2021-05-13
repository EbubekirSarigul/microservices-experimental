using FluentValidation;

namespace Player.Core.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("EMPTY_NAME").WithMessage("Name is required.")
                                .MaximumLength(50).WithErrorCode("INVALID_NAME").WithMessage("Name is too long.");
            RuleFor(x => x.Surname).NotEmpty().WithErrorCode("EMPTY_SURNAME").WithMessage("Surname is too long..")
                                .MaximumLength(50).WithErrorCode("INVALID_DATE").WithMessage("Surname is too long.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("EMPTY_PHONE_NUMBER").WithMessage("Phone number is required.")
                                       .MaximumLength(20).WithErrorCode("INVALID_PHONE_NUMBER").WithMessage("Phone number is invalid.");
            RuleFor(x => x.Rating).NotEmpty().WithErrorCode("EMPTY_RATING").WithMessage("Rating is required.");
        }
    }
}

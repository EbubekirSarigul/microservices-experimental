using FluentValidation;

namespace Basket.Core.Commands.Checkout
{
    public class CheckoutCommandValidation : AbstractValidator<CheckoutCommand>
    {
        public CheckoutCommandValidation()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithErrorCode("EMPTY_PLAYER_ID").WithMessage("Player id is required.");
        }
    }
}

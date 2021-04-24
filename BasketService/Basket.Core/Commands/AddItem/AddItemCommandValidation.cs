using FluentValidation;

namespace Basket.Core.Commands.AddItem
{
    public class AddItemCommandValidation : AbstractValidator<AddItemCommand>
    {
        public AddItemCommandValidation()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithErrorCode("EMPTY_PLAYER_ID").WithMessage("Player id is required.");
            RuleFor(x => x.Tournament).NotEmpty().WithErrorCode("EMPTY_TOURNAMENT").WithMessage("Tournament is required.");
            RuleFor(x => x.Tournament).SetValidator(new TournamentValidator());
        }
    }
}

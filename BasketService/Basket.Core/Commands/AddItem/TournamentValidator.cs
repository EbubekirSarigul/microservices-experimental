using Basket.Core.Models;
using FluentValidation;

namespace Basket.Core.Commands.AddItem
{
    public class TournamentValidator : AbstractValidator<Tournament>
    {
        public TournamentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithErrorCode("EMPTY_TOURNAMENT_ID").WithMessage("Tournament id is required.");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("EMPTY_NAME").WithMessage("Name is required.")
                    .MaximumLength(50).WithErrorCode("INVALID_NAME").WithMessage("Name is too long.");
            RuleFor(x => x.Price).InclusiveBetween(0, 100000).WithErrorCode("INVALID_ENTRY_PRICE").WithMessage("Maximum entry price is 1000");
        }
    }
}

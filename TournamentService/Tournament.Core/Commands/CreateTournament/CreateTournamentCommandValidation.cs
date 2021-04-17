using FluentValidation;
using System;
using System.Globalization;

namespace Tournament.Core.Commands.CreateTournament
{
    public class CreateTournamentCommandValidation: AbstractValidator<CreateTournamentCommand>
    {
        public CreateTournamentCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("EMPTY_NAME").WithMessage("Name is required.")
                                .MaximumLength(50).WithErrorCode("INVALID_NAME").WithMessage("Name is too long.");
            RuleFor(x => x.Date).NotEmpty().WithErrorCode("EMPTY_DATE").WithMessage("Date is required.")
                                .Must(BeAValidDate).WithErrorCode("INVALID_DATE").WithMessage("Date format is invalid.");
            RuleFor(x => x.Address).MaximumLength(200).WithErrorCode("INVALID_ADDRESS").WithMessage("Address is too long.");
            RuleFor(x => x.EntryPrice).InclusiveBetween(0, 100000).WithErrorCode("INVALID_ENTRY_PRICE").WithMessage("Maximum entry price is 1000");
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result);

        }
    }
}

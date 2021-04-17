using FluentValidation;
using System;
using System.Globalization;

namespace Tournament.Core.Commands.UpdateTournament
{
    public class UpdateTournamentCommandValidation : AbstractValidator<UpdateTournamentCommand>
    {
        public UpdateTournamentCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithErrorCode("EMPTY_ID").WithMessage("Id is required.")
                            .Must(BeAValidGuid).WithErrorCode("INVALID_ID_FORMAT").WithMessage("Id must be a valid guid.");
            RuleFor(x => x.Date).NotEmpty().WithErrorCode("EMPTY_DATE").WithMessage("Date is required.")
                                .Must(BeAValidDate).WithErrorCode("INVALID_DATE").WithMessage("Date format is invalid.");
            RuleFor(x => x.EntryPrice).InclusiveBetween(0, 100000).WithErrorCode("INVALID_ENTRY_PRICE").WithMessage("Maximum entry price is 1000");
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result);

        }

        private bool BeAValidGuid(string guid)
        {
            return Guid.TryParse(guid, out var result);
        }
    }
}

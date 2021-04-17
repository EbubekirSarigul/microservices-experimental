using MicroserviceTraining.Framework.Commands;

namespace Tournament.Core.Commands.CreateTournament
{
    public class CreateTournamentCommand : BaseCommand<CreateTournamentResult>
    {
        public string Name { get; set; }

        public string Date { get; set; }

        public int EntryPrice { get; set; }

        public string Address { get; set; }
    }
}

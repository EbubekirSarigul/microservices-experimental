using MicroserviceTraining.Framework.Commands;

namespace Tournament.Core.Commands.UpdateTournament
{
    public class UpdateTournamentCommand : BaseCommand<UpdateTournamentResult>
    {
        public string Id { get; set; }

        public string Date { get; set; }

        public int EntryPrice { get; set; }
    }
}

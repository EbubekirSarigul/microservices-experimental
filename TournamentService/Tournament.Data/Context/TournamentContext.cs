using MediatR;
using MicroserviceTraining.Framework.Data;
using MicroserviceTraining.Framework.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Data.EntityConfigurations;
using Entity = Tournament.Data.Entities;

namespace Tournament.Data.Context
{
    public class TournamentContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Entity.Participant> Participant { get; set; }

        public DbSet<Entity.Tournament> Tournament { get; set; }

        public TournamentContext(DbContextOptions<TournamentContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TournamentConfiguration());
            builder.ApplyConfiguration(new ParticipantConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            //Domain events published just after committing changes.
            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }
    }
}

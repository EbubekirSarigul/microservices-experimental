using MediatR;
using MicroserviceTraining.Framework.Data;
using MicroserviceTraining.Framework.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Player.Data.EntityConfigurations;
using System.Threading;
using System.Threading.Tasks;

namespace Player.Data.Context
{
    public class PlayerContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Entities.Player> Players { get; set; }

        public PlayerContext(DbContextOptions<PlayerContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlayerConfiguration());
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

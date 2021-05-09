using MediatR;
using MicroserviceTraining.Framework.Data;
using MicroserviceTraining.Framework.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Player.Data.Entities;
using Player.Data.EntityConfigurations;
using System.Threading;
using System.Threading.Tasks;

namespace Player.Data.Context
{
    public class PaymentContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Orders> Orders { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OrdersConfiguration());
            builder.ApplyConfiguration(new OrderDetailsConfiguration());
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

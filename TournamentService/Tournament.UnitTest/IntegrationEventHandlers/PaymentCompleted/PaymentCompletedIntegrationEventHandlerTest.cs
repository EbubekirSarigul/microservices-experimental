using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Core.IntegrationEventHandlers.PaymentCompleted;
using Tournament.Data.Context;
using Tournament.Data.Repositories;

namespace Tournament.UnitTest.IntegrationEventHandlers.PaymentCompleted
{
    [TestFixture]
    public class PaymentCompletedIntegrationEventHandlerTest
    {
        [Test]
        public async Task SuccessCase()
        {
            string connString = "Server=localhost;Port=3306;Database=dft;Uid=root;Pwd=Aa123456;";

            var dbContextOptions = new DbContextOptionsBuilder<TournamentContext>()
                                                                .UseMySql(connString, ServerVersion.AutoDetect(connString))
                                                                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll).Options;

            PaymentCompletedIntegrationEventHandler sut = new PaymentCompletedIntegrationEventHandler(new TournamentRepository(
                new TournamentContext(dbContextOptions, Mock.Of<IMediator>())));

            await sut.Handle(new PaymentCompletedIntegrationEvent
            {
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid(),
                PlayerId = Guid.Parse("f13ec8f6-c4e0-4fe9-9dcc-399dae89d368"),
                Tournaments = new System.Collections.Generic.List<Guid>()
                {
                    Guid.Parse("5586f747-b886-4d71-951a-e477c2cd7107")
                }
            }, default(CancellationToken));
        }
    }
}

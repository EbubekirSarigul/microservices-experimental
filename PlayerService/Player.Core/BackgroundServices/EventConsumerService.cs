﻿using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using MicroserviceTraining.Framework.IOC;
using Microsoft.Extensions.Hosting;
using Player.Core.IntegrationEventHandlers.NewTournamentAdded;
using System.Threading;
using System.Threading.Tasks;

namespace Player.Core.BackgroundServices
{
    public class EventConsumerService : BackgroundService
    {
        private readonly IEventBus _eventBus = IocFacility.Container.Resolve<IEventBus>();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield(); // note : a new thread could also be used instead of Task.Yield(); OR completele different project(A Worker Service for example)

            _eventBus.Subscribe<NewTournamentAddedIntegrationEvent>(Constant.EventTopic_TournamentAdded);

            await _eventBus.StartConsuming(stoppingToken);
        }
    }
}

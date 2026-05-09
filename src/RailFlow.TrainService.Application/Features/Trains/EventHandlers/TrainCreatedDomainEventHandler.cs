using MediatR;

using Microsoft.Extensions.Logging;

using RailFlow.Contracts.Events;
using RailFlow.TrainService.Application.Common.Behaviours;
using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Trains.Events;

namespace RailFlow.TrainService.Application.Features.Trains.EventHandlers;

public sealed class TrainCreatedDomainEventHandler
    : INotificationHandler<DomainEventNotification<TrainCreatedDomainEvent>>
{
    private readonly ILogger<TrainCreatedDomainEventHandler> _logger;
    private readonly IEventBus _eventBus;

    public TrainCreatedDomainEventHandler( ILogger<TrainCreatedDomainEventHandler> logger, IEventBus eventBus )
    {
        this._logger = logger;
        this._eventBus = eventBus;
    }

    public async Task Handle( DomainEventNotification<TrainCreatedDomainEvent> notification, CancellationToken ct )
    {
        TrainCreatedDomainEvent domainEvent = notification.DomainEvent;

        TrainCreatedIntegrationEvent integrationEvent = new(domainEvent.TrainId, domainEvent.Number, domainEvent.OccurredOnUtc);

        await this._eventBus.PublishAsync( integrationEvent, ct );

        this._logger.LogInformation(
            "Train created: {TrainId} - {Number}",
            domainEvent.TrainId,
            domainEvent.Number );
    }
}


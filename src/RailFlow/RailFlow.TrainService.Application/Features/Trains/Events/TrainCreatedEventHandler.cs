using MediatR;

using Microsoft.Extensions.Logging;

using RailFlow.TrainService.Application.Common.Behaviours;
using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Application.Features.Trains.Events;

public sealed class TrainCreatedEventHandler
    : INotificationHandler<DomainEventNotification<TrainCreatedEvent>>
{
    private readonly ILogger<TrainCreatedEventHandler> _logger;

    public TrainCreatedEventHandler( ILogger<TrainCreatedEventHandler> logger )
    {
        this._logger = logger;
    }

    public Task Handle( DomainEventNotification<TrainCreatedEvent> notification, CancellationToken ct )
    {
        TrainCreatedEvent trainCreatedEvent = notification.DomainEvent;

        this._logger.LogInformation(
            "Train created: {TrainId} - {Number}",
            trainCreatedEvent.TrainId,
            trainCreatedEvent.Number );

        return Task.CompletedTask;
    }
}


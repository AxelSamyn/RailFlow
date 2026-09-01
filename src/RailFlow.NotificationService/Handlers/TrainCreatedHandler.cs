using RailFlow.Contracts.Events;
using RailFlow.NotificationService.Common.Interfaces.Handlers;

namespace RailFlow.NotificationService.Handlers;

public sealed class TrainCreatedHandler : IIntegrationEventHandler<TrainCreatedIntegrationEvent>
{
    private readonly ILogger<TrainCreatedHandler> _logger;
    public TrainCreatedHandler( ILogger<TrainCreatedHandler> logger )
    {
        this._logger = logger;
    }
    public Task HandleAsync( TrainCreatedIntegrationEvent @event, CancellationToken cancellationToken )
    {
        this._logger.LogInformation(
            "Received TrainCreatedEvent: {TrainId} - {Number}",
            @event.Id,
            @event.Number );
        // Here you would add logic to send notifications, e.g., via email or push notifications.
        return Task.CompletedTask;
    }
}

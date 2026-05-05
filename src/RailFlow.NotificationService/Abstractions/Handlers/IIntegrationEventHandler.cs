namespace RailFlow.NotificationService.Abstractions.Handlers;

public interface IIntegrationEventHandler<in TEvent>
{
    Task HandleAsync( TEvent @event, CancellationToken cancellationToken );
}

public interface IIntegrationEventHandler
{
    Task HandleAsync( object @event, CancellationToken ct );
}

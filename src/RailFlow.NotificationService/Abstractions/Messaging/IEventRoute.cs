namespace RailFlow.NotificationService.Abstractions.Messaging;

public interface IEventRoute
{
    string EventType { get; }
    Task HandleAsync( string payload, IServiceProvider serviceProvider, CancellationToken cancellationToken );
}

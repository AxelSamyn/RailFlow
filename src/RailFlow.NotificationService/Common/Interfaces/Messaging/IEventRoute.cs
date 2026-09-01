namespace RailFlow.NotificationService.Common.Interfaces.Messaging;

public interface IEventRoute
{
    string EventType { get; }
    Task HandleAsync( string payload, IServiceProvider serviceProvider, CancellationToken cancellationToken );
}

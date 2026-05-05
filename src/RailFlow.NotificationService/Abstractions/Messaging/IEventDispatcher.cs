using RailFlow.Contracts.Events;

namespace RailFlow.NotificationService.Abstractions.Messaging;

public interface IEventDispatcher
{
    Task DispatchAsync( IntegrationEventEnvelope envelope, CancellationToken cancellationToken );
}

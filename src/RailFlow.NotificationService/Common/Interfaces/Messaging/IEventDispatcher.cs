using RailFlow.Contracts.Events;

namespace RailFlow.NotificationService.Common.Interfaces.Messaging;

public interface IEventDispatcher
{
    Task DispatchAsync( IntegrationEventEnvelope envelope, CancellationToken cancellationToken );
}

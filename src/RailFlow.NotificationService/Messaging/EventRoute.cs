using System.Text.Json;

using RailFlow.Contracts.Abstractions.Events;
using RailFlow.NotificationService.Abstractions.Handlers;
using RailFlow.NotificationService.Abstractions.Messaging;
using RailFlow.NotificationService.Configuration;
using RailFlow.NotificationService.Messaging.Exceptions;

namespace RailFlow.NotificationService.Messaging;

public sealed class EventRoute<T> : IEventRoute
    where T : IIntegrationEvent
{
    public string EventType => T.EventType;

    public async Task HandleAsync( string payload, IServiceProvider serviceProvider, CancellationToken cancellationToken )
    {
        T evt;

        try
        {
            evt = JsonSerializer.Deserialize<T>( payload, JsonDefaults.Options )
                ?? throw new NonRetryableException( $"Payload deserialized to null for event type '{typeof( T ).Name}'." );
        }
        catch ( JsonException ex )
        {
            throw new NonRetryableException( $"Invalid payload for event type '{typeof( T ).Name}'.", ex );
        }

        IEnumerable<IIntegrationEventHandler<T>> handlers = serviceProvider.GetServices<IIntegrationEventHandler<T>>();

        foreach ( IIntegrationEventHandler<T> handler in handlers )
        {
            await handler.HandleAsync( evt, cancellationToken );
        }
    }

}

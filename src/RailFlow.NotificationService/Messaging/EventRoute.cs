using System.Text.Json;

using RailFlow.NotificationService.Abstractions.Handlers;
using RailFlow.NotificationService.Abstractions.Messaging;
using RailFlow.NotificationService.Configuration;

namespace RailFlow.NotificationService.Messaging;

public sealed class EventRoute<T> : IEventRoute
{
    public string EventType { get; }

    public EventRoute( string eventType )
    {
        EventType = eventType;
    }

    public async Task HandleAsync( string payload, IServiceProvider serviceProvider, CancellationToken cancellationToken )
    {
        T? @event = JsonSerializer.Deserialize<T>( payload, JsonDefaults.Options );

        if ( @event is null )
            throw new InvalidOperationException( $"Invalid payload for {typeof( T ).Name}" );

        IEnumerable<IIntegrationEventHandler<T>> handlers = serviceProvider.GetServices<IIntegrationEventHandler<T>>( );

        foreach ( IIntegrationEventHandler<T> handler in handlers )
        {
            await handler.HandleAsync( @event, cancellationToken );
        }
    }

}

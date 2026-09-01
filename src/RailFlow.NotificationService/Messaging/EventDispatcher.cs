using System.Text.Json;

using RailFlow.Contracts.Events;
using RailFlow.NotificationService.Common.Interfaces.Messaging;
using RailFlow.NotificationService.Messaging.Exceptions;

namespace RailFlow.NotificationService.Messaging;

public sealed class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, IEventRoute> _routes;
    private readonly ILogger<EventDispatcher> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public EventDispatcher( IServiceProvider serviceProvider, ILogger<EventDispatcher> logger, IEnumerable<IEventRoute> routes )
    {
        this._serviceProvider = serviceProvider;
        this._logger = logger;
        this._routes = routes.ToDictionary( r => r.EventType );

    }

    public async Task DispatchAsync( IntegrationEventEnvelope envelope, CancellationToken ct )
    {
        // Here, we could also consider using a more robust routing mechanism, such as a message broker or a mediator pattern, to decouple the event producers and consumers even further.
        // This would allow for more flexible and scalable event handling, especially as the number of event types and handlers grows.
        if ( !this._routes.TryGetValue( envelope.Type, out IEventRoute? route ) )
        {
            throw new NonRetryableException( $"No route found for event type '{envelope.Type}'." );
        }

        await route.HandleAsync( envelope.Payload, this._serviceProvider, ct );
    }
}

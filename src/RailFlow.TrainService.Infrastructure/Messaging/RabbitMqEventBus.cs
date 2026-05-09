using System.Text;
using System.Text.Json;

using RabbitMQ.Client;

using RailFlow.Contracts.Abstractions.Events;
using RailFlow.Contracts.Events;
using RailFlow.TrainService.Application.Common.Interfaces;

namespace RailFlow.TrainService.Infrastructure.Messaging;

internal class RabbitMqEventBus : IEventBus, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    private const string ExchangeName = "railflow.events";

    public RabbitMqEventBus( )
    {
        ConnectionFactory factory = new( )
        {
            HostName = "localhost"
        };

        this._connection = factory.CreateConnectionAsync( ).GetAwaiter( ).GetResult( );
        this._channel = this._connection.CreateChannelAsync( ).GetAwaiter( ).GetResult( );
    }

    public async Task PublishAsync<T>( T integrationEvent, CancellationToken cancellationToken )
        where T : IIntegrationEvent
    {
        await this._channel.ExchangeDeclareAsync(
            exchange: ExchangeName,
            durable: true,
            type: ExchangeType.Fanout,
            cancellationToken: cancellationToken
        );

        //CorrelationId should originate from request boundary
        IntegrationEventEnvelope envelope = new(
            Type: T.EventType,
            Payload: JsonSerializer.Serialize( integrationEvent ),
            CorrelationId: Guid.NewGuid( ).ToString( ),
            OccurredAtUtc: DateTime.UtcNow
            );

        string message = JsonSerializer.Serialize(envelope);
        byte[ ] body = Encoding.UTF8.GetBytes( message );

        await this._channel.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: string.Empty,
            mandatory: false,
            body: body,
            cancellationToken: cancellationToken
        );
    }

    public async ValueTask DisposeAsync( )
    {
        await this._channel.DisposeAsync( );
        await this._connection.DisposeAsync( );
    }
}

using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using RailFlow.Contracts.Abstractions.Events;
using RailFlow.Contracts.Events;
using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Infrastructure.Configuration;

namespace RailFlow.TrainService.Infrastructure.Messaging;

internal class RabbitMqEventBus : IEventBus, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly ICorrelationContext _correlationContext;

    private const string ExchangeName = "railflow.events";

    public RabbitMqEventBus( IOptions<RabbitMqOptions> rabbitMqOptions, ICorrelationContext correlationContext )
    {
        this._rabbitMqOptions = rabbitMqOptions.Value;

        ConnectionFactory factory = new( )
        {
            HostName = this._rabbitMqOptions.Host,
            Port = this._rabbitMqOptions.Port,
            UserName = this._rabbitMqOptions.User,
            Password = this._rabbitMqOptions.Password
        };

        this._connection = factory.CreateConnectionAsync( ).GetAwaiter( ).GetResult( );
        this._channel = this._connection.CreateChannelAsync( ).GetAwaiter( ).GetResult( );
        this._correlationContext = correlationContext;
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
            CorrelationId: this._correlationContext.CorrelationId ?? Guid.NewGuid( ).ToString( "D" ),
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

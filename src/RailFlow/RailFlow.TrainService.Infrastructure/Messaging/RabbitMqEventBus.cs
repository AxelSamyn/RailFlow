using System.Text;
using System.Text.Json;

using RabbitMQ.Client;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Infrastructure.Messaging;

internal class RabbitMqEventBus : IEventBus, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqEventBus( )
    {
        ConnectionFactory factory = new( )
        {
            HostName = "localhost"
        };

        this._connection = factory.CreateConnectionAsync( ).GetAwaiter( ).GetResult( );
        this._channel = this._connection.CreateChannelAsync( ).GetAwaiter( ).GetResult( );
    }

    public async Task PublishAsync( IDomainEvent domainEvent, CancellationToken cancellationToken )
    {
        _ = domainEvent.GetType( ).Name;

        await this._channel.ExchangeDeclareAsync(
            exchange: "railflow.events",
            type: ExchangeType.Fanout,
            cancellationToken: cancellationToken
        );

        string message = JsonSerializer.Serialize(domainEvent);
        byte[ ] body = Encoding.UTF8.GetBytes( message );

        await this._channel.BasicPublishAsync(
            exchange: "railflow.events",
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

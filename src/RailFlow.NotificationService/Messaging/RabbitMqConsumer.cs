using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RailFlow.NotificationService.Messaging;

public class RabbitMqConsumer : BackgroundService
{
    private IConnection? _connection;
    private IChannel? _channel;

    protected override async Task ExecuteAsync( CancellationToken cancellationToken )
    {
        ConnectionFactory factory = new( )
        {
            HostName = "localhost"
        };

        this._connection = await factory.CreateConnectionAsync( );
        this._channel = await this._connection.CreateChannelAsync( );

        await this._channel.ExchangeDeclareAsync(
            exchange: "railflow.events",
            type: ExchangeType.Fanout,
            cancellationToken: cancellationToken
            );

        var queue = await this._channel.QueueDeclareAsync(
            durable: false,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken
            );

        await this._channel.QueueBindAsync(
            queue: queue.QueueName,
            exchange: "railflow.events",
            routingKey: string.Empty,
            cancellationToken: cancellationToken
            );

        AsyncEventingBasicConsumer consumer = new( this._channel );

        consumer.ReceivedAsync += async ( sender, args ) => {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine( "📩 Event received:" );
            Console.WriteLine( message );

            await Task.CompletedTask;
        };

        _ = await this._channel.BasicConsumeAsync(
            queue: queue.QueueName,
            autoAck: true,
            consumer: consumer,
            cancellationToken: cancellationToken
            );
    }
}

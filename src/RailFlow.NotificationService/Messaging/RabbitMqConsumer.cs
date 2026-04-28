using System.Text;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RailFlow.NotificationService.Messaging;

public class RabbitMqConsumer : BackgroundService
{
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly ConnectionFactory _connectionFactory;

    private IConnection? _connection;
    private IChannel? _channel;

    private const string ExchangeName = "railflow.events";
    private const string QueueName = "railflow.notifications";

    public RabbitMqConsumer( IOptions<RabbitMqOptions> options, ILogger<RabbitMqConsumer> logger )
    {
        this._rabbitMqOptions = options.Value;
        this._logger = logger;

        this._connectionFactory = new ConnectionFactory
        {
            HostName = this._rabbitMqOptions.Host,
            Port = this._rabbitMqOptions.Port,
            UserName = this._rabbitMqOptions.User,
            Password = this._rabbitMqOptions.Password
        };
    }

    protected override async Task ExecuteAsync( CancellationToken cancellationToken )
    {

        int attempt = 1;
        int delaySeconds = 1;
        int delayFactor = 2;
        int MAX_DELAY_SECONDS = 15;

        while ( cancellationToken.IsCancellationRequested is not true )
        {
            try
            {
                this._logger.LogInformation( "Connecting to RabbitMQ..." );

                await ConnectWithRetryAsync( attempt, cancellationToken );

                this._logger.LogInformation( "Connected to RabbitMQ" );

                await StartConsumerAsync( cancellationToken );

                this._logger.LogInformation( "Consumer started. Waiting for messages..." );

                await WaitUntilDisconnectedAsync( cancellationToken );

                this._logger.LogWarning( "RabbitMQ connection lost. Reconnecting..." );
            }
            catch ( Exception ex )
            {
                if ( attempt % 5 == 0 )
                {
                    this._logger.LogWarning( ex,
                        "RabbitMQ unavailable after {Attempt} attempts. Retrying...",
                        attempt );
                }
                else
                {
                    this._logger.LogInformation(
                        "RabbitMQ unavailable (attempt {Attempt}). Retrying...",
                        attempt );
                }

                await Task.Delay( TimeSpan.FromSeconds( delaySeconds ), cancellationToken );
            }
            finally
            {
                await CleanupAsync( );
            }

            attempt++;
            delaySeconds = Math.Min( MAX_DELAY_SECONDS, delaySeconds * delayFactor );
        }
    }

    private async Task HandleMessageAsync( string message, CancellationToken token ) => this._logger.LogInformation( "Handling message {Message}", message );

    private async Task ConnectWithRetryAsync( int attempt, CancellationToken cancellationToken )
    {
        this._connection = await this._connectionFactory.CreateConnectionAsync( cancellationToken );
        this._channel = await this._connection.CreateChannelAsync( null, cancellationToken );
    }

    private async Task StartConsumerAsync( CancellationToken cancellationToken )
    {
        ArgumentNullException.ThrowIfNull( this._channel );

        await this._channel.ExchangeDeclareAsync(
            exchange: ExchangeName,
            type: ExchangeType.Fanout,
            durable: true,
            cancellationToken: cancellationToken
            );

        _ = await this._channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken
            );

        await this._channel.QueueBindAsync(
            queue: QueueName,
            exchange: ExchangeName,
            routingKey: string.Empty,
            cancellationToken: cancellationToken
            );

        AsyncEventingBasicConsumer consumer = new( this._channel );

        consumer.ReceivedAsync += async ( sender, args ) => {
            try
            {
                string message = Encoding.UTF8.GetString(args.Body.ToArray( ));

                this._logger.LogInformation( "Event received {Message}", message );

                await HandleMessageAsync( message, cancellationToken );

                await this._channel.BasicAckAsync( args.DeliveryTag, false, cancellationToken );
            }
            catch ( Exception ex )
            {
                this._logger.LogError( ex, "Error processing message" );

                // Option : dead-letter ou retry
                await this._channel.BasicNackAsync( args.DeliveryTag, false, true, cancellationToken );
            }
        };

        _ = await this._channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken
            );
    }

    private async Task WaitUntilDisconnectedAsync( CancellationToken cancellationToken )
    {
        ArgumentNullException.ThrowIfNull( this._connection );

        TaskCompletionSource<bool> tcs = new();

        this._connection.ConnectionShutdownAsync += ( _, _ ) => {
            _ = tcs.TrySetResult( true );
            return Task.CompletedTask;
        };

        _ = await tcs.Task.WaitAsync( cancellationToken );
    }

    private async Task CleanupAsync( )
    {
        try
        {
            if ( this._channel is not null )
            {
                await this._channel.CloseAsync( );
                await this._channel.DisposeAsync( );
            }

            if ( this._connection is not null )
            {
                await this._connection.CloseAsync( );
                await this._connection.DisposeAsync( );
            }
        }
        catch ( Exception ex )
        {
            this._logger.LogWarning( ex, "Error during cleanup" );
        }
    }
}

using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using RailFlow.Contracts.Events;
using RailFlow.NotificationService.Abstractions.Messaging;
using RailFlow.NotificationService.Common.Logging;
using RailFlow.NotificationService.Configuration;

namespace RailFlow.NotificationService.Messaging;

public class RabbitMqConsumer : BackgroundService
{
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly ConnectionFactory _connectionFactory;
    private readonly IEventDispatcher _dispatcher;

    private IConnection? _connection;
    private IChannel? _channel;

    private const string ExchangeName = "railflow.events";
    private const string QueueName = "railflow.notifications";

    private CancellationToken _stoppingToken;

    public RabbitMqConsumer( IOptions<RabbitMqOptions> options, ILogger<RabbitMqConsumer> logger, IEventDispatcher dispatcher )
    {
        this._rabbitMqOptions = options.Value;
        this._logger = logger;
        this._dispatcher = dispatcher;

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
        this._stoppingToken = cancellationToken;

        int attempt = 1;
        int delaySeconds = 1;
        int delayFactor = 2;
        int MAX_DELAY_SECONDS = 15;

        while ( cancellationToken.IsCancellationRequested is not true )
        {
            try
            {
                this._logger.LogInformation( "Connecting to RabbitMQ..." );

                // Attempt to connect to RabbitMQ.
                // If it fails, it will throw an exception which we catch to implement the retry logic.
                await ConnectAsync( attempt, cancellationToken );

                this._logger.LogInformation( "Connected to RabbitMQ" );

                // Once connected, we start consuming messages.
                // This will run until the connection is lost or the service is stopped.
                await StartConsumerAsync( cancellationToken );

                this._logger.LogInformation( "Consumer started. Waiting for messages..." );

                // Wait until the connection is lost.
                // This will block until the connection shutdown event is triggered.
                // If the connection is lost, we will exit this method
                // and go back to the retry loop to attempt reconnection.
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

    private async Task ConnectAsync( int attempt, CancellationToken cancellationToken )
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

        consumer.ReceivedAsync += HandleMessageAsync;

        _ = await this._channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken
            );
    }

    private async Task HandleMessageAsync( object sender, BasicDeliverEventArgs args )
    {
        ArgumentNullException.ThrowIfNull( this._channel );

        try
        {
            //First, we need to deserialize the message body into our IntegrationEventEnvelope
            string message = Encoding.UTF8.GetString(args.Body.ToArray());

            IntegrationEventEnvelope? envelope = JsonSerializer.Deserialize<IntegrationEventEnvelope>( message, JsonDefaults.Options );

            if ( envelope is null )
            {
                this._logger.LogWarning( "Invalid message format" );
                await this._channel.BasicNackAsync( args.DeliveryTag, false, false );
                return;
            }

            //Then, we can dispatch the event to our handlers. We use a logging scope to include the CorrelationId in all logs related to this message.
            using ( this._logger.BeginCorrelationScope( envelope.CorrelationId ) )
            {
                await this._dispatcher.DispatchAsync( envelope, this._stoppingToken );
            }

            //Finally, we acknowledge the message to RabbitMQ to indicate that it has been processed successfully.
            await this._channel.BasicAckAsync( args.DeliveryTag, false );
        }
        catch ( Exception ex )
        {
            this._logger.LogError( ex, "Error processing message" );

            await this._channel.BasicNackAsync( args.DeliveryTag, false, true );
        }
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

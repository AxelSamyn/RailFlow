using RailFlow.Contracts.Events;
using RailFlow.NotificationService.Common.Interfaces.Correlation;
using RailFlow.NotificationService.Common.Interfaces.Handlers;
using RailFlow.NotificationService.Common.Interfaces.Messaging;
using RailFlow.NotificationService.Configuration;
using RailFlow.NotificationService.Correlation;
using RailFlow.NotificationService.Handlers;
using RailFlow.NotificationService.Messaging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders( );
builder.Logging.AddConsole( );

builder.Configuration
    .AddJsonFile( "appsettings.json", optional: false )
    .AddJsonFile( $"appsettings.{builder.Environment.EnvironmentName}.json", optional: true )
    .AddEnvironmentVariables( );

builder.Services.AddSingleton<AsyncLocalCorrelationContext>( );
builder.Services.AddSingleton<ICorrelationContext>( provider => provider.GetRequiredService<AsyncLocalCorrelationContext>( ) );
builder.Services.AddSingleton<ICorrelationContextAccessor>( provider => provider.GetRequiredService<AsyncLocalCorrelationContext>( ) );

builder.Services.AddSingleton<IEventRoute>( new EventRoute<TrainCreatedIntegrationEvent>( ) );
//builder.Services.AddSingleton<IEventRoute>(new EventRoute<TrainCreatedEvent>() );
builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>( );

builder.Services.AddTransient<IIntegrationEventHandler<TrainCreatedIntegrationEvent>, TrainCreatedHandler>( );

builder.Services
    .AddOptions<RabbitMqOptions>( )
    .Bind( builder.Configuration.GetSection( "RabbitMq" ) )
    .ValidateDataAnnotations( )
    .Validate( o => !string.IsNullOrWhiteSpace( o.Host ), "Host is required" )
    .Validate( o => o.Port > 0, "Port must be greater than 0" )
    .Validate( o => !string.IsNullOrWhiteSpace( o.User ), "User is required" )
    .Validate( o => !string.IsNullOrWhiteSpace( o.Password ), "Password is required" )
    .ValidateOnStart( );
builder.Services.AddHostedService<RabbitMqConsumer>( );

IHost host = builder.Build();
host.Run( );

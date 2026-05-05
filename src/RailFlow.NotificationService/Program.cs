using RailFlow.Contracts.Events;
using RailFlow.NotificationService.Abstractions.Handlers;
using RailFlow.NotificationService.Abstractions.Messaging;
using RailFlow.NotificationService.Configuration;
using RailFlow.NotificationService.Handlers;
using RailFlow.NotificationService.Messaging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders( );
builder.Logging.AddConsole( );

builder.Services.AddSingleton<IEventRoute>( new EventRoute<TrainCreatedEvent>( "train.created" ) );
//builder.Services.AddSingleton<IEventRoute>(new EventRoute<TrainCreatedEvent>("train.created") );
builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>( );

builder.Services.AddTransient<IIntegrationEventHandler<TrainCreatedEvent>, TrainCreatedHandler>( );

builder.Services.Configure<RabbitMqOptions>( builder.Configuration.GetSection( "RabbitMq" ) );
builder.Services.AddHostedService<RabbitMqConsumer>( );

IHost host = builder.Build();
host.Run( );

using RailFlow.NotificationService;
using RailFlow.NotificationService.Messaging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders( );
builder.Logging.AddConsole( );

builder.Services.Configure<RabbitMqOptions>( builder.Configuration.GetSection( "RabbitMq" ) );
builder.Services.AddHostedService<RabbitMqConsumer>( );

IHost host = builder.Build();
host.Run( );

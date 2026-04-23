using RailFlow.NotificationService;
using RailFlow.NotificationService.Messaging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>( );
builder.Services.AddHostedService<RabbitMqConsumer>( );

IHost host = builder.Build();
host.Run( );

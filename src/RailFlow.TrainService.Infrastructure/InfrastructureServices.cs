using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Trains;
using RailFlow.TrainService.Infrastructure.Configuration;
using RailFlow.TrainService.Infrastructure.Messaging;
using RailFlow.TrainService.Infrastructure.Persistence;
using RailFlow.TrainService.Infrastructure.Persistence.Repositories;

namespace RailFlow.TrainService.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration config )
    {
        _ = services
            .AddOptions<RabbitMqOptions>( )
            .Bind( config.GetSection( "RabbitMq" ) )
            .ValidateDataAnnotations( )
            .Validate( o => !string.IsNullOrWhiteSpace( o.Host ), "Host is required" )
            .Validate( o => o.Port > 0, "Port must be greater than 0" )
            .Validate( o => !string.IsNullOrWhiteSpace( o.User ), "User is required" )
            .Validate( o => !string.IsNullOrWhiteSpace( o.Password ), "Password is required" )
            .ValidateOnStart( );

        _ = services.AddDbContext<TrainDbContext>( options =>
            options.UseSqlServer( config.GetConnectionString( "TrainDb" ) ) );

        _ = services.AddScoped<ITrainDbContext>( provider => provider.GetRequiredService<TrainDbContext>( ) );
        _ = services.AddScoped<ITrainRepository, TrainRepository>( );

        _ = services.AddScoped<IEventBus, RabbitMqEventBus>( );

        return services;
    }
}

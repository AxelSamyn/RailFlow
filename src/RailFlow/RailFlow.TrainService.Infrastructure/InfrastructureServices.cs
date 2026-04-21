using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Trains;
using RailFlow.TrainService.Infrastructure.Persistence;
using RailFlow.TrainService.Infrastructure.Persistence.Repositories;

namespace RailFlow.TrainService.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration config )
    {
        _ = services.AddDbContext<TrainDbContext>( options =>
            options.UseSqlServer( config.GetConnectionString( "TrainDb" ) ) );

        _ = services.AddScoped<ITrainDbContext>( provider => provider.GetRequiredService<TrainDbContext>( ) );
        _ = services.AddScoped<ITrainRepository, TrainRepository>( );

        return services;
    }
}

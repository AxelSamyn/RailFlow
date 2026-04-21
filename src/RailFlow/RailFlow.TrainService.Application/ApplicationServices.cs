using Microsoft.Extensions.DependencyInjection;

using RailFlow.TrainService.Application.Common.Behaviours;
using RailFlow.TrainService.Application.Common.Interfaces;

namespace RailFlow.TrainService.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices( this IServiceCollection services )
    {
        _ = services.AddScoped<IDomainEventDispatcher, MediatoRDomainEventDispatcher>( );
        return services;
    }
}

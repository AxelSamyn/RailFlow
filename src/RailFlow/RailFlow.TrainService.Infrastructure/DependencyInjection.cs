using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RailFlow.TrainService.Application.Abstractions.Persistence;
using RailFlow.TrainService.Application.Features.Trains.CreateTrain;
using RailFlow.TrainService.Infrastructure.Persistence;
using RailFlow.TrainService.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TrainDbContext>(options => 
                options.UseSqlServer(config.GetConnectionString("TrainDb")));

            services.AddScoped<ITrainDbContext>(provider => provider.GetRequiredService<TrainDbContext>());
            services.AddScoped<ITrainRepository, TrainRepository>();


            return services;
        }
    }
}

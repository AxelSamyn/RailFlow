using Microsoft.EntityFrameworkCore;
using RailFlow.TrainService.Application.Abstractions.Persistence;
using RailFlow.TrainService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Infrastructure.Persistence
{
    public class TrainDbContext : DbContext, ITrainDbContext
    {
        public TrainDbContext(DbContextOptions<TrainDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Train> Trains => Set<Train>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => base.SaveChangesAsync(cancellationToken);
    }
}

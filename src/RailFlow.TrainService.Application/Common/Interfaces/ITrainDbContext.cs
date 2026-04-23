using Microsoft.EntityFrameworkCore;

using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Application.Common.Interfaces;

public interface ITrainDbContext
{
    DbSet<Train> Trains { get; }

    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}

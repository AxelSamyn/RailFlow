using RailFlow.TrainService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Application.Abstractions.Persistence
{
    public interface ITrainDbContext
    {
        DbSet<Train> Trains { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

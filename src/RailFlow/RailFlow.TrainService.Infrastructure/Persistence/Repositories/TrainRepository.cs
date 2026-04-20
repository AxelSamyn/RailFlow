using RailFlow.TrainService.Application.Features.Trains.CreateTrain;
using RailFlow.TrainService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Infrastructure.Persistence.Repositories
{
    public class TrainRepository : ITrainRepository
    {
        private readonly TrainDbContext _context;

        public TrainRepository(TrainDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Train train)
        {
            _context.Trains.Add(train);
            await _context.SaveChangesAsync();
        }
    }
}

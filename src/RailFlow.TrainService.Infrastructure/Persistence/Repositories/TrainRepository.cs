using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Infrastructure.Persistence.Repositories;

public class TrainRepository : ITrainRepository
{
    private readonly TrainDbContext _context;

    public TrainRepository( TrainDbContext context )
    {
        this._context = context;
    }

    public async Task AddAsync( Train train )
    {
        _ = this._context.Trains.Add( train );
        _ = await this._context.SaveChangesAsync( );
    }
}

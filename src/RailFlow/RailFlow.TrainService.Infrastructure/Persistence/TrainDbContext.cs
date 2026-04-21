using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Common;
using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Infrastructure.Persistence;

public class TrainDbContext : DbContext, ITrainDbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public TrainDbContext( DbContextOptions<TrainDbContext> options, IDomainEventDispatcher dispatcher )
        : base( options )
    {
        this._dispatcher = dispatcher;
    }

    public DbSet<Train> Trains => Set<Train>( );

    public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default )
    {
        IEnumerable<EntityEntry<BaseEntity>> entities =
            ChangeTracker
                .Entries<BaseEntity>( );

        //Save changes to the database first
        int result = await base.SaveChangesAsync( cancellationToken );

        var domainEvents = entities
            .SelectMany( x => x.Entity.DomainEvents )
            .ToList( );

        //dispatch domain events after saving changes to the database
        await this._dispatcher.DispatchAsync( domainEvents, cancellationToken );

        //clear domain events after dispatching
        foreach ( EntityEntry<BaseEntity> entity in entities )
        {
            entity.Entity.ClearDomainEvents( );
        }

        return result;
    }
}

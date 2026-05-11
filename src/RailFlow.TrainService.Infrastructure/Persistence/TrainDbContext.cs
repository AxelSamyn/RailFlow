using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Common;
using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Infrastructure.Persistence;

public class TrainDbContext : DbContext, ITrainDbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    /// <summary>
    /// Constructor used at runtime.
    /// </summary>
    public TrainDbContext( DbContextOptions<TrainDbContext> options, IDomainEventDispatcher dispatcher )
        : base( options )
    {
        this._dispatcher = dispatcher;
    }

    /// <summary>
    /// Constructor used by EF Core design-time tooling.
    /// </summary>
    public TrainDbContext( DbContextOptions<TrainDbContext> options )
        : base( options )
    {
    }

    public DbSet<Train> Trains => Set<Train>( );

    public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default )
    {
        ArgumentNullException.ThrowIfNull( this._dispatcher, nameof( this._dispatcher ) );

        List<EntityEntry<BaseEntity>> entities =
            ChangeTracker
                .Entries<BaseEntity>( )
                .ToList();

        List<IDomainEvent> domainEvents =
            ChangeTracker
                .Entries<BaseEntity>( )
                .Where(e =>
                    e.State is   EntityState.Added   or
                      EntityState.Modified  )
                .SelectMany( x => x.Entity.DomainEvents )
                .ToList( );

        //Save changes to the database first
        int result = await base.SaveChangesAsync( cancellationToken );

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

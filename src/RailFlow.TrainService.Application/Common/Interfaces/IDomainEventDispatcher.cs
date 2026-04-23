using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Application.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync( IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken );
}

using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Domain.Trains.Events;

public sealed record TrainCreatedDomainEvent( Guid TrainId, string Number ) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}

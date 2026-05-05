using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Domain.Trains;

public sealed record TrainCreatedEvent( Guid TrainId, string Number ) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}

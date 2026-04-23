namespace RailFlow.TrainService.Domain.Common;

public interface IDomainEvent
{
    DateTime OccuredOnUtc { get; }
}

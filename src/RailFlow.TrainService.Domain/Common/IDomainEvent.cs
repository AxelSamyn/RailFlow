namespace RailFlow.TrainService.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}

using RailFlow.Contracts.Abstractions.Events;

namespace RailFlow.Contracts.Events;

public sealed record TrainCreatedIntegrationEvent(
    Guid Id,
    string Number,
    DateTime OccurredOnUtc
) : IIntegrationEvent
{
    public static string EventType => "train.created";

}

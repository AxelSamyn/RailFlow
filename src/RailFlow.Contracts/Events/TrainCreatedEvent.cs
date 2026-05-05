namespace RailFlow.Contracts.Events;

public sealed record TrainCreatedEvent(
    Guid Id,
    string Number,
    DateTime OccurredOnUtc
);

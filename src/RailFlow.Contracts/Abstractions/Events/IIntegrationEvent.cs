namespace RailFlow.Contracts.Abstractions.Events;

public interface IIntegrationEvent
{
    static abstract string EventType { get; }
    DateTime OccurredOnUtc { get; }
}

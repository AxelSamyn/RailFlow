namespace RailFlow.Contracts.Events;

public sealed record IntegrationEventEnvelope(
    string Type,
    string Payload,
    string? CorrelationId,
    DateTime OccurredAtUtc
);

using RailFlow.NotificationService.Common.Interfaces.Correlation;

namespace RailFlow.NotificationService.Correlation;

public sealed class AsyncLocalCorrelationContext : ICorrelationContextAccessor
{
    private static readonly AsyncLocal<string?> _correlationIdStorage = new();

    public string? CorrelationId => _correlationIdStorage.Value;

    public void SetCorrelationId( string? correlationId ) => _correlationIdStorage.Value = correlationId;
    public void Clear( ) => _correlationIdStorage.Value = null;
}

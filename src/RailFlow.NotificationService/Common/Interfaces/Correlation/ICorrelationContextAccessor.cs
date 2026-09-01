namespace RailFlow.NotificationService.Common.Interfaces.Correlation;

public interface ICorrelationContextAccessor : ICorrelationContext
{
    void SetCorrelationId( string? correlationId );

    void Clear( );
}

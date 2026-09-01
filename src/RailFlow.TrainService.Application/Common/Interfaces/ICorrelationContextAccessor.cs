namespace RailFlow.TrainService.Application.Common.Interfaces;

public interface ICorrelationContextAccessor : ICorrelationContext
{
    void SetCorrelationId( string correlationId );
    void Clear( );
}

namespace RailFlow.NotificationService.Common.Interfaces.Correlation;

public interface ICorrelationContext
{
    string? CorrelationId { get; }
}

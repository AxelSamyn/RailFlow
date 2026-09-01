namespace RailFlow.TrainService.Application.Common.Interfaces;

public interface ICorrelationContext
{
    string? CorrelationId { get; }
}

using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Application.Common.Interfaces;

public interface IEventBus
{
    Task PublishAsync( IDomainEvent domainEvent, CancellationToken cancellationToken = default );
}

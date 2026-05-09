using RailFlow.Contracts.Abstractions.Events;

namespace RailFlow.TrainService.Application.Common.Interfaces;

public interface IEventBus
{
    Task PublishAsync<T>( T integrationEvent, CancellationToken cancellationToken = default )
        where T : IIntegrationEvent;
}

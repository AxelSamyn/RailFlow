using MediatR;

using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Application.Common.Behaviours;

public sealed class DomainEventNotification<T> : INotification
    where T : IDomainEvent
{
    public T DomainEvent { get; }

    public DomainEventNotification( T domainEvent )
    {
        DomainEvent = domainEvent;
    }
}

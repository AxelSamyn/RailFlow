using MediatR;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Application.Common.Behaviours;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly IEventBus _eventBus;

    public DomainEventDispatcher( IMediator mediator, IEventBus eventBus )
    {
        this._mediator = mediator;
        this._eventBus = eventBus;
    }
    public async Task DispatchAsync( IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken )
    {
        foreach ( IDomainEvent domainEvent in domainEvents )
        {
            INotification notification = CreateNotification( domainEvent );

            await this._mediator.Publish( notification, cancellationToken );
            await this._eventBus.PublishAsync( domainEvent, cancellationToken );
        }
    }

    public static INotification CreateNotification( IDomainEvent domainEvent )
    {
        Type notificationType = typeof( DomainEventNotification<> ).MakeGenericType( domainEvent.GetType( ) );

        var notification = Activator.CreateInstance(notificationType, domainEvent);

        return notification as INotification ?? throw new InvalidOperationException( "Failed to create notification." );
    }
}

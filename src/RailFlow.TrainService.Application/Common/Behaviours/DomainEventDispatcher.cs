using MediatR;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Application.Common.Behaviours;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher( IMediator mediator )
    {
        this._mediator = mediator;
    }
    public async Task DispatchAsync( IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken )
    {
        foreach ( IDomainEvent domainEvent in domainEvents )
        {
            INotification notification = CreateNotification( domainEvent );

            await this._mediator.Publish( notification, cancellationToken );
        }
    }

    public static INotification CreateNotification( IDomainEvent domainEvent )
    {
        Type notificationType = typeof( DomainEventNotification<> ).MakeGenericType( domainEvent.GetType( ) );

        return (INotification)Activator.CreateInstance(
            notificationType,
            domainEvent )!;

        //object? notification = Activator.CreateInstance( notificationType, domainEvent );

        //return notification as INotification ?? throw new InvalidOperationException( "Failed to create notification." );
    }
}

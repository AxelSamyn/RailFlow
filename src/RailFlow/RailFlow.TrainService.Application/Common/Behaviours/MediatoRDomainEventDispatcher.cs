using MediatR;

using RailFlow.TrainService.Application.Common.Interfaces;
using RailFlow.TrainService.Domain.Common;

namespace RailFlow.TrainService.Application.Common.Behaviours;

public sealed class MediatoRDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public MediatoRDomainEventDispatcher( IMediator mediator )
    {
        this._mediator = mediator;
    }
    public async Task DispatchAsync( IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken )
    {
        foreach ( IDomainEvent domainEvent in domainEvents )
        {
            Type notificationType = typeof( DomainEventNotification<> ).MakeGenericType( domainEvent.GetType() );

            object? notification = Activator.CreateInstance( notificationType, domainEvent );

            if ( notification is not INotification mediatorNotification )
            {
                throw new InvalidOperationException( $"Failed to create notification for domain event {domainEvent.GetType( ).Name}" );
            }

            await this._mediator.Publish(
                mediatorNotification,
                cancellationToken
            );
        }
    }

    public Task DispatchAsync<T>( IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken ) => throw new NotImplementedException( );
}

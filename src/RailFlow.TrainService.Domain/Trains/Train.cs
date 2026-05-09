using RailFlow.TrainService.Domain.Common;
using RailFlow.TrainService.Domain.Trains.Events;

namespace RailFlow.TrainService.Domain.Trains;

public sealed class Train : BaseEntity
{
    public Guid Id { get; private set; }
    public string Number { get; private set; }

    private Train( ) { }

    public Train( string number )
    {
        Id = Guid.NewGuid( );
        Number = number;

        AddDomainEvent( new TrainCreatedDomainEvent( Id, Number ) );
    }
}

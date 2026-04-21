using MediatR;

namespace RailFlow.TrainService.Application.Features.Trains.CreateTrain;

public record CreateTrainCommand( string Number ) : IRequest<Guid>
{
}

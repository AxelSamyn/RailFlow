using MediatR;

namespace RailFlow.TrainService.Application.Features.Trains.GetTrainById;

public record GetTrainByIdQuery( Guid Id ) : IRequest<TrainDto>
{
}

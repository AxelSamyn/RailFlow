using MediatR;

using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Application.Features.Trains.CreateTrain;

public class CreateTrainHandler : IRequestHandler<CreateTrainCommand, Guid>
{
    private readonly ITrainRepository _trainRepository;
    public CreateTrainHandler( ITrainRepository trainRepository )
    {
        this._trainRepository = trainRepository;
    }
    public async Task<Guid> Handle( CreateTrainCommand request, CancellationToken cancellationToken )
    {
        Train train = new(request.Number);
        await this._trainRepository.AddAsync( train );

        train.ClearDomainEvents( );
        return train.Id;
    }
}

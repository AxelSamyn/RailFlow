using MediatR;

using Microsoft.Extensions.Logging;

using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Application.Features.Trains.CreateTrain;

public class CreateTrainHandler : IRequestHandler<CreateTrainCommand, Guid>
{
    private readonly ITrainRepository _trainRepository;
    private readonly ILogger<CreateTrainHandler> _logger;
    public CreateTrainHandler( ITrainRepository trainRepository, ILogger<CreateTrainHandler> logger )
    {
        this._trainRepository = trainRepository;
        this._logger = logger;
    }
    public async Task<Guid> Handle( CreateTrainCommand request, CancellationToken cancellationToken )
    {
        this._logger.LogInformation( "Creating train with number {Number}", request.Number );

        Train train = new(request.Number);
        await this._trainRepository.AddAsync( train );

        train.ClearDomainEvents( );
        return train.Id;
    }
}

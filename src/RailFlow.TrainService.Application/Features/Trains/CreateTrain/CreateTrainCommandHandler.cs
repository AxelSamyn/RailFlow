using MediatR;

using Microsoft.Extensions.Logging;

using RailFlow.TrainService.Domain.Trains;

namespace RailFlow.TrainService.Application.Features.Trains.CreateTrain;

public class CreateTrainCommandHandler : IRequestHandler<CreateTrainCommand, Guid>
{
    private readonly ITrainRepository _trainRepository;
    private readonly ILogger<CreateTrainCommandHandler> _logger;
    public CreateTrainCommandHandler( ITrainRepository trainRepository, ILogger<CreateTrainCommandHandler> logger )
    {
        this._trainRepository = trainRepository;
        this._logger = logger;
    }
    public async Task<Guid> Handle( CreateTrainCommand request, CancellationToken cancellationToken )
    {
        this._logger.LogInformation( "Creating train with number {Number}", request.Number );

        Train train = new(request.Number);
        await this._trainRepository.AddAsync( train );
        return train.Id;
    }
}

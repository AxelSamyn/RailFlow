using MediatR;
using RailFlow.TrainService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Application.Features.Trains.CreateTrain
{
    public class CreateTrainHandler : IRequestHandler<CreateTrainCommand, Guid>
    {
        private readonly ITrainRepository _trainRepository;
        public CreateTrainHandler(ITrainRepository trainRepository)
        {
            _trainRepository = trainRepository;
        }
        public async Task<Guid> Handle(CreateTrainCommand request, CancellationToken cancellationToken)
        {
            var train = new Train(request.Number);
            await _trainRepository.AddAsync(train);
            return train.Id;
        }
    }
}

using RailFlow.TrainService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Application.Features.Trains.CreateTrain
{
    public interface ITrainRepository
    {
        Task AddAsync(Train train);
    }
}

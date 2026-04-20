using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Application.Features.Trains.GetTrainById
{
    public record TrainDto(Guid Id, string Number);
}

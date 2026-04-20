using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Application.Features.Trains.GetTrainById
{
    
    public record GetTrainByIdQuery(Guid Id) : IRequest<TrainDto>
    {
    }
}

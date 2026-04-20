using MediatR;
using Microsoft.EntityFrameworkCore;
using RailFlow.TrainService.Application.Abstractions.Persistence;

namespace RailFlow.TrainService.Application.Features.Trains.GetTrainById
{
    public class GetTrainByIdHandler : IRequestHandler<GetTrainByIdQuery, TrainDto?>
    {
        private readonly ITrainDbContext _context;

        public GetTrainByIdHandler(ITrainDbContext context)
        {
            _context = context;
        }

        public async Task<TrainDto?> Handle(GetTrainByIdQuery request, CancellationToken token)
        {
            return await _context.Trains
                .Where(t => t.Id == request.Id)
                .Select(t => new TrainDto(t.Id, t.Number))
                .FirstOrDefaultAsync(token);
        }
    }
}

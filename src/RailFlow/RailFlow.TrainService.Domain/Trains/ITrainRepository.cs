namespace RailFlow.TrainService.Domain.Trains;

public interface ITrainRepository
{
    Task AddAsync( Train train );
}

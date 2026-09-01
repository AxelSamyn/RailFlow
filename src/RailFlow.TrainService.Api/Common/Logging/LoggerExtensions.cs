namespace RailFlow.TrainService.Api.Common.Logging;

public static class LoggerExtensions
{
    public sealed class NoopDisposable : IDisposable
    {
        public static readonly NoopDisposable Instance = new();

        public void Dispose( ) { }
    }

    public static IDisposable BeginCorrelationScope(
        this ILogger logger,
        string? correlationId )
    {
        return logger.BeginScope( new Dictionary<string, object?>
        {
            ["CorrelationId"] = correlationId
        } ) ?? NoopDisposable.Instance;
    }
}

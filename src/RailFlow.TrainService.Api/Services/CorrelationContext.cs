using RailFlow.TrainService.Application.Common.Interfaces;

namespace RailFlow.TrainService.Api.Services;

public class CorrelationContext : ICorrelationContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationContext( IHttpContextAccessor httpContextAccessor )
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string? CorrelationId => this._httpContextAccessor.HttpContext?.Items["CorrelationId"] as string;
}

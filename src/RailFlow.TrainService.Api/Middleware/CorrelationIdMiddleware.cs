using Microsoft.Extensions.Primitives;

using RailFlow.TrainService.Application.Common.Interfaces;

namespace RailFlow.TrainService.Api.Middleware;

public sealed class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;
    private const string CorrelationIdHeaderName = "X-Correlation-Id";
    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    public CorrelationIdMiddleware( RequestDelegate next, ILogger<CorrelationIdMiddleware> logger, ICorrelationContextAccessor correlationContextAccessor )
    {
        this._next = next;
        this._logger = logger;
        this._correlationContextAccessor = correlationContextAccessor;
    }

    public async Task InvokeAsync( HttpContext context )
    {
        string correlationId =  context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out StringValues headerValues) &&
            !StringValues.IsNullOrEmpty(headerValues)
            ?  headerValues.ToString()
            :  Guid.NewGuid().ToString("D") ;

        this._logger.LogInformation( "Correlation middleware generated {CorrelationId}", correlationId );

        // Expose correlation id to response and ASP.NET tracing
        context.Response.Headers[CorrelationIdHeaderName] = correlationId;
        context.TraceIdentifier = correlationId;

        // Optional: make it available to controllers/middleware
        //context.Items["CorrelationId"] = correlationId;
        this._correlationContextAccessor.SetCorrelationId( correlationId );

        // Begin logging scope so all logs contain CorrelationId when logging provider includes scopes
        using ( this._logger.BeginScope( "CorrelationId:{CorrelationId}", correlationId ) )
        {
            this._logger.LogInformation( "Inside correlation scope" );

            await this._next( context );
        }
    }
}
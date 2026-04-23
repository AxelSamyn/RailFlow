using MediatR;

using RailFlow.TrainService.Application;
using RailFlow.TrainService.Application.Features.Trains.CreateTrain;
using RailFlow.TrainService.Application.Features.Trains.GetTrainById;
using RailFlow.TrainService.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers( );
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi( );

builder.Services.AddMediatR( cfg => cfg.RegisterServicesFromAssembly( typeof( CreateTrainHandler ).Assembly ) );

builder.Services.AddApplicationServices( );
builder.Services.AddInfrastructureServices( builder.Configuration );
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment( ) )
{
    _ = app.MapOpenApi( );
}

app.UseHttpsRedirection( );

app.UseAuthorization( );

app.MapPost( "/trains", async ( CreateTrainCommand command, IMediator mediator ) => {
    Guid trainId = await mediator.Send(command);
    return Results.Ok( trainId );
} );

app.MapGet( "trains/{id:guid}", async ( Guid id, IMediator mediator ) => {
    TrainDto? result = await mediator.Send( new GetTrainByIdQuery( id ) );

    return result is not null
        ? Results.Ok( result )
        : Results.NotFound( );
} );
//app.MapControllers();

app.UseSwagger( );
app.UseSwaggerUI( );

app.Run( );

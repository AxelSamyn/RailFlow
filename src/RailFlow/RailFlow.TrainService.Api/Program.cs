using MediatR;
using Microsoft.EntityFrameworkCore;
using RailFlow.TrainService.Application.Features.Trains.CreateTrain;
using RailFlow.TrainService.Application.Features.Trains.GetTrainById;
using RailFlow.TrainService.Infrastructure;
using RailFlow.TrainService.Infrastructure.Persistence;
using RailFlow.TrainService.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTrainHandler).Assembly));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPost("/trains", async (CreateTrainCommand command, IMediator mediator) =>
{
    var trainId = await mediator.Send(command);
    return Results.Ok(trainId);
});

app.MapGet("trains/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetTrainByIdQuery(id));

    return result is not null
        ? Results.Ok(result)
        : Results.NotFound();
});
//app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();

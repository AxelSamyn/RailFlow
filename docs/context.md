# RailFlow - Context

## Goal
Build a distributed system demo to showcase senior .NET skills

## Stack
- .NET 10
- ASP.NET Core API
- Blazor (future)
- SQL Server (Docker)
- RabbitMQ (planned)

## Architecture
- Clean Architecture (Domain / Application / Infrastructure / API)
- Vertical Slice Architecture (Application layer)
- CQRS (commands + queries)
- MediatR for use cases
- Repository pattern for writes only
- Direct DbContext for reads

## Current State
- Train entity implemented
- CreateTrain + GetTrainById working
- EF Core + SQL Server running in Docker
- Dependency Injection configured
- Swagger working
- ADR 001 → 012 written

## Next Step
- Introduce Domain Events
- Then integrate RabbitMQ

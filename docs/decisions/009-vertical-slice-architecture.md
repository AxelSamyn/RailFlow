# ADR 009: Vertical Slice Architecture

## Status
Accepted

## Context
Traditional layered architectures tend to scatter logic across multiple layers, making features harder to understand and maintain.

We want to organize the system around business use cases.

## Decision

We adopt Vertical Slice Architecture in the Application layer.

Each feature is structured as:

- Command or Query
- Handler
- DTOs (if needed)

Example:

Features/
  Trains/
    CreateTrain/
      CreateTrainCommand.cs
      CreateTrainHandler.cs
    GetTrainById/
      GetTrainByIdQuery.cs
      GetTrainByIdHandler.cs

## Consequences

### Positive
- High cohesion per feature
- Easy navigation
- Reduced cognitive load
- Better alignment with business use cases

### Negative
- Some duplication across features
- Requires discipline to avoid shared abstractions too early

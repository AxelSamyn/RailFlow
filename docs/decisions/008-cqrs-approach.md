# ADR 008: CQRS Strategy

## Status
Accepted

## Context
The system is designed as a distributed architecture where scalability, separation of concerns, and clarity of intent are important.

We need to decide how to handle read and write operations.

## Decision

We adopt Command Query Responsibility Segregation (CQRS):

- Commands modify state and go through the domain model
- Queries read data and are optimized for performance

### Write side
- Uses domain entities
- Uses repository pattern (ITrainRepository)
- Enforces business rules

### Read side
- Uses direct DbContext access
- Uses projections (DTOs)
- No repository abstraction

### Mediation
- All interactions go through MediatR
- Each use case has its own handler

## Consequences

### Positive
- Clear separation of responsibilities
- Better scalability (read/write independently scalable later)
- Simplified query logic
- Domain integrity preserved

### Negative
- Duplication between read and write models
- More files (one per use case)

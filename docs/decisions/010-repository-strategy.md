# ADR 010: Repository Strategy

## Status
Accepted

## Context
In a CQRS architecture, the role of repositories differs between read and write operations.

We must decide how to use repositories without over-engineering.

## Decision

We adopt a hybrid repository strategy:

### Write side
- Use repository pattern (ITrainRepository)
- Encapsulates persistence logic for domain entities
- Ensures domain integrity

### Read side
- No repository abstraction
- Direct use of DbContext
- Queries are implemented in handlers

## Rationale
- Repositories add value for domain behavior (writes)
- Repositories add little value for read operations
- Avoid unnecessary abstraction

## Consequences

### Positive
- Reduced boilerplate
- Clear separation between read and write concerns
- Simpler query implementation

### Negative
- Mixed patterns in the codebase
- Requires discipline to maintain consistency

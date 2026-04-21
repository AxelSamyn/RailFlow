# ADR 012: MediatR Usage Policy

## Status
Accepted

## Context
We use MediatR to decouple application logic from controllers and orchestrate use cases.

We must define how and when it is used.

## Decision

- All application use cases go through MediatR
- Each command/query has a dedicated handler
- Controllers do not contain business logic

### Usage rules

- One handler per use case
- Handlers are thin and focused
- No orchestration logic in controllers

### Scope

- MediatR is used only in the Application layer
- Not used inside Domain layer

## Rationale

- Promotes separation of concerns
- Improves testability
- Aligns with Vertical Slice Architecture

## Consequences

### Positive
- Clear flow of execution
- Easy to test use cases
- Decoupled architecture

### Negative
- Increased number of files
- Learning curve for new developers

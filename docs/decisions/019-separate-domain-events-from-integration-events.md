# ADR 019: Separate Domain Events from Integration Events

## Status
Accepted

## Context
The system uses Domain-Driven Design concepts and event-driven communication between services.

Some events are internal to the domain and should not be exposed outside the service boundary, while others are intended for inter-service communication.

## Decision
We separate events into two categories:

- Domain Events
  - Used internally inside the service
  - Triggered by domain entities
  - Dispatched through MediatR

- Integration Events
  - Used for communication between services
  - Published through RabbitMQ
  - Represent external contracts

Domain Events may be mapped explicitly to Integration Events.

## Consequences

### Positive
- Clear separation of concerns
- Preserves domain encapsulation
- Reduces coupling between services
- Enables evolution of internal domain logic without breaking external consumers

### Negative
- Additional mapping layer
- More event types to maintain

## Alternatives considered
- Using the same events internally and externally (rejected due to coupling concerns)
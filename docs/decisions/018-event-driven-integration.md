# ADR 018: Adopt Event-Driven Communication Between Services

## Status
Accepted

## Context
The system aims to scale and decouple services handling different responsibilities.

## Decision
We adopt an event-driven architecture:
- Commands handled synchronously
- Events published asynchronously
- Consumers react independently

## Consequences

### Positive
- Loose coupling
- Better scalability
- Easier extension of features

### Negative
- Increased complexity
- Eventual consistency

## Alternatives considered
- Synchronous API calls (rejected: tight coupling)
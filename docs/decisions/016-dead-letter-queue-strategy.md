# ADR 016: Introduce Dead Letter Queue for Failed Messages

## Status
Accepted

## Context
Some messages may fail permanently (invalid data, bugs, incompatibility).

## Decision
A Dead Letter Queue (DLQ) is used:
- Messages are routed to DLQ when marked as non-retryable
- Main queue defines DLQ via x-dead-letter-* arguments

## Consequences

### Positive
- Prevents infinite retry loops
- Isolates problematic messages
- Enables later analysis

### Negative
- Requires monitoring and manual handling

## Alternatives considered
- Infinite retry (rejected: can block system)
- Discard messages (rejected: data loss)
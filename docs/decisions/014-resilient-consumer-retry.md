# ADR 014: Implement Infinite Retry with Backoff in Consumers

## Status
Accepted

## Context
Consumers may start before RabbitMQ is available or may lose connection during runtime.

## Decision
Consumers implement an infinite retry mechanism with exponential backoff.

The retry:
- Never stops unless the service is shut down
- Uses increasing delays (2s → 30s max)
- Logs warnings instead of errors during retry

## Consequences

### Positive
- High resilience
- No crash loops
- Self-healing behavior

### Negative
- Slight delay before recovery
- Requires careful logging to avoid noise

## Alternatives considered
- Fixed retry count (rejected: not resilient enough)
- Fail fast (rejected: unsuitable for background workers)
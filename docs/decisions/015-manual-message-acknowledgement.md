# ADR 015: Use Manual Acknowledgement for Messages

## Status
Accepted

## Context
Automatic acknowledgement (autoAck) may lead to message loss if processing fails.

## Decision
Consumers use manual acknowledgement:
- BasicAck on success
- BasicNack on failure

## Consequences

### Positive
- Reliable message processing
- No data loss on failure
- Enables retry strategies

### Negative
- Slightly more complex implementation

## Alternatives considered
- autoAck = true (rejected due to risk of message loss)
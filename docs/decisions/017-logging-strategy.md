# ADR 017: Use Microsoft ILogger with Structured Logging

## Status
Accepted

## Context
The system requires consistent logging across services for debugging and observability.

## Decision
We use Microsoft.Extensions.Logging with:
- Console provider
- Structured logging (message templates)
- Log levels (Information, Warning, Error)

## Consequences

### Positive
- Standard .NET logging
- Easy integration with cloud platforms (Azure)
- Structured logs for analysis

### Negative
- Limited advanced features compared to Serilog (can be added later)

## Alternatives considered
- Serilog (deferred)
- Console.WriteLine (rejected)
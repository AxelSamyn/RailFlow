# ADR 002: Do Not Use .NET Aspire (Initial Phase)

## Status

Accepted

## Context

.NET Aspire provides abstractions and tooling to simplify distributed application development.

However, the goal of this project is to explicitly demonstrate understanding of distributed system fundamentals.

## Decision

Do not use .NET Aspire in the initial implementation.

Instead, manually configure:

* Service communication
* Infrastructure (Docker, RabbitMQ, databases)
* Application wiring

## Consequences

### Positive

* Clear visibility into system architecture
* Better demonstration of technical skills
* No hidden abstractions
* Easier to explain in interviews

### Negative

* More manual setup required
* Slightly slower initial setup

## Notes

Aspire may be introduced in a later phase for comparison purposes.

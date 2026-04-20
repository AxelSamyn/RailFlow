# ADR 004: Use RabbitMQ for Message Broker

## Status

Accepted

## Context

The system requires asynchronous communication between services.

A message broker is needed to simulate real-world distributed system behavior.

## Decision

Use RabbitMQ as the message broker for local development.

Abstract messaging logic to allow future migration to cloud solutions such as Azure Service Bus.

## Consequences

### Positive

* Easy local setup via Docker
* Widely used and well-documented
* Good fit for event-driven architecture
* No cloud cost

### Negative

* Differences with Azure Service Bus features
* Additional abstraction layer required for portability

## Notes

Cloud messaging services may be introduced in later phases.

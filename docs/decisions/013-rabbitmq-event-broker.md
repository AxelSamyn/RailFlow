# ADR 013: Use RabbitMQ as Event Broker

## Status
Accepted

## Context
The system requires asynchronous communication between services to decouple business operations and improve scalability.

## Decision
We use RabbitMQ as the message broker for inter-service communication.

RabbitMQ is configured using:
- Fanout exchanges for event broadcasting
- Durable queues for reliability
- Docker-based deployment for local development

## Consequences

### Positive
- Decoupled architecture
- Asynchronous processing
- Industry-standard messaging system
- Easy local setup with Docker

### Negative
- Additional infrastructure complexity
- Requires message handling strategy (retry, DLQ)

## Alternatives considered
- Azure Service Bus (deferred for cloud phase)
- Direct HTTP communication (rejected due to tight coupling)
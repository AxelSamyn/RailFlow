# ADR 006: Use Docker for Local Infrastructure

## Status

Accepted

## Context

The project requires multiple infrastructure components:

* Database
* Message broker

Consistency across environments is important.

## Decision

Use Docker and Docker Compose to manage local infrastructure.

Application services will initially run locally and be containerized later.

## Consequences

### Positive

* Reproducible development environment
* Easy onboarding
* Isolation of dependencies
* Industry standard approach

### Negative

* Requires Docker knowledge
* Adds initial setup complexity

## Notes

Full containerization of services will be introduced in later stages.

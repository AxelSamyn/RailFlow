# ADR 007: Use Event Sourcing Selectively

## Status

Proposed

## Context

Event sourcing is a powerful pattern for distributed systems but introduces complexity.

## Decision

Apply event sourcing only to selected parts of the system (e.g., Train Service), if needed.

Do not adopt it system-wide.

## Consequences

### Positive

* Demonstrates advanced architectural knowledge
* Enables replay and audit capabilities
* Limits complexity to critical areas

### Negative

* Increased complexity for affected services
* Requires additional infrastructure

## Notes

This decision will be revisited after initial system stabilization.

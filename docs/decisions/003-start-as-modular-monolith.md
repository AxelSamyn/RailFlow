# ADR 003: Start with a Modular Monolith

## Status

Accepted

## Context

The project aims to demonstrate a distributed system architecture.

However, building distributed systems directly from scratch introduces complexity and risk.

## Decision

Start with a modular monolith using:

* Clean Architecture
* CQRS
* Vertical Slice Architecture

Then progressively evolve into a distributed system.

## Consequences

### Positive

* Faster initial development
* Easier debugging
* Solid domain foundation before distribution
* Reduced architectural risk

### Negative

* Requires later refactoring to split services
* Temporary mismatch with final architecture vision

## Notes

This approach aligns with industry best practices: "start simple, evolve to distributed".

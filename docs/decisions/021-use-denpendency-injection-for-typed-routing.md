# ADR 021: Use Dependency Injection for Typed Event Routing

## Status
Accepted

## Context
Integration events must be routed dynamically to their corresponding handlers while preserving strong typing.

The system should remain extensible without requiring modifications to central routing logic.

## Decision
Event routes are registered through dependency injection.

The dispatcher:
- Resolves all registered routes at startup
- Builds an in-memory dictionary indexed by event type
- Delegates payload deserialization and handler resolution to typed routes

Each route is implemented using a generic EventRoute<T> abstraction.

## Consequences

### Positive
- Strongly typed event handling
- Open/Closed Principle compliance
- Easy addition of new integration events
- Clear separation between routing and handling

### Negative
- Additional abstraction layer
- Requires route registration for each integration event

## Alternatives considered
- Large switch/case routing logic (rejected due to maintainability concerns)
- Reflection-based automatic routing (rejected due to reduced readability)
# ADR 005: Use Blazor Web App for Frontend

## Status

Accepted

## Context

The project requires a frontend to demonstrate system interactions.

Multiple Blazor hosting models are available.

## Decision

Use Blazor Web App (.NET 8+) with server-side rendering and interactive components.

## Consequences

### Positive

* Modern and recommended approach
* Faster initial load compared to WebAssembly
* Better integration with backend APIs
* Suitable for enterprise applications

### Negative

* Requires server hosting
* Less offline capability than WebAssembly

## Notes

The frontend is intentionally simple and focuses on demonstrating backend interactions.

# ADR 001: Use SQL Server as Primary Database

## Status

Accepted

## Context

The project requires a relational database to support transactional operations and structured data storage across services.

The developer has strong experience with SQL Server, and the goal of the project is to deliver a high-quality demonstration within a limited timeframe.

## Decision

Use SQL Server as the primary database for all services.

Each service will own its own database schema to ensure proper separation of concerns.

## Consequences

### Positive

* Faster development due to familiarity
* Strong tooling support (EF Core, Visual Studio)
* Industry-proven technology widely used in enterprise environments
* Reduced learning curve during critical development phase

### Negative

* Less alignment with some cloud-native stacks favoring PostgreSQL
* Slightly heavier compared to lightweight alternatives

## Notes

The choice prioritizes delivery speed and reliability over exploration of new database technologies.

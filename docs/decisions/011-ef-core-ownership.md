# ADR 011: EF Core Ownership Strategy

## Status
Accepted

## Context
Entity Framework Core is used as the ORM. We must define its boundaries and impact on the domain model.

## Decision

- EF Core is used in the Infrastructure layer
- The Application layer depends on an abstraction (ITrainDbContext)
- Domain entities are plain C# objects (POCOs)

### DbContext exposure
- Exposed via ITrainDbContext
- Used directly in query handlers

### Entity design
- Domain entities are persistence-aware but not persistence-driven
- Minimal EF Core attributes or configuration inside domain

### Tracking
- EF tracking is allowed for write operations
- Queries use AsNoTracking by default

## Consequences

### Positive
- Clear separation of concerns
- Infrastructure remains replaceable
- Domain model remains clean

### Negative
- Some coupling remains due to EF materialization
- Requires discipline in mapping and configuration

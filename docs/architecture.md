# RailFlow — Architecture Overview

## 🎯 Objective

RailFlow is a distributed system demo designed to showcase modern backend architecture skills using .NET, including:

- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- Vertical Slice Architecture
- Event-driven communication
- Distributed system design
- Observability and resilience patterns

The goal is to demonstrate technical expertise in building scalable, maintainable, and cloud-ready systems.

---

## 🚆 Domain Overview

RailFlow simulates a railway logistics system where trains transport cargo between stations.

### Core Concepts

- **Train**: Represents a transport unit
- **Station**: A location where events occur
- **Cargo**: Goods transported by trains
- **Segment**: A portion of a journey
- **Incident**: Unexpected events (delays, failures)

---

## 🧩 System Architecture

The system is composed of multiple services:

### 1. Train Service

- Source of truth for train lifecycle
- Handles commands (CreateTrain, UpdateStatus)
- Emits domain events
- Publishes integration events via RabbitMQ

### 2. Notification Service

- RabbitMQ consumer (Worker Service)
- Subscribes to domain/integration events
- Processes messages asynchronously
- Implements retry, reconnection, and message acknowledgement

### 3. Tracking Service (Planned)

- Builds read models
- Consumes events from Train Service
- Provides query endpoints

### 4. Incident Service (Planned)

- Handles anomalies (delays, failures)
- Reacts to system events
- Emits incident-related events

### 5. Web Application (Planned)

- Blazor Web App (.NET 8+)
- Displays system state
- Sends commands via API

---

## 🧱 Architectural Patterns

### Clean Architecture

- Separation of concerns
- Domain-centric design
- Infrastructure isolated from business logic

### CQRS

- Commands and Queries are separated
- Write model optimized for business logic
- Read model optimized for queries

### Vertical Slice Architecture

- Features organized by use case
- Each slice contains its own logic, validation, and handler

---

## 📡 Communication

### In-process (Current)

- Domain events via MediatR

### Asynchronous Messaging

- RabbitMQ as message broker
- Fanout exchanges for event broadcasting
- Consumers implemented as background services
- Manual message acknowledgement (ACK/NACK)
- Retry strategy with exponential backoff
- Dead-letter queue support (planned)

---

## 🗄️ Data Storage

- SQL Server (per service)
- Each service owns its database
- No shared database between services

---

## 🐳 Infrastructure (Local Development)

Managed via Docker Compose:

- SQL Server
- RabbitMQ
- Notification Service (Worker)

Applications are containerized progressively.

---

## 🔁 Resilience

- Infinite retry with exponential backoff (consumer)
- Graceful reconnection to RabbitMQ
- Manual message acknowledgement
- Dead-letter queue strategy (planned)

---

## 📊 Observability

- Microsoft.Extensions.Logging (structured logging)
- Container-friendly logging (Docker logs)
- Correlation IDs (planned)
- OpenTelemetry (planned)

---

## 🧪 Testing Strategy

- Unit tests (Domain)
- Integration tests (API)
- Event-driven tests (planned)
- Contract testing (planned)

---

## 🚀 Deployment Strategy (Planned)

- Dockerized services
- CI/CD pipeline (GitHub Actions)
- Azure-ready architecture:

  - Azure Container Apps / AKS
  - Azure Service Bus (alternative to RabbitMQ)
  - Azure SQL

---

## 🧭 Development Roadmap

### Phase 1 — Monolith foundation

- Clean Architecture
- CQRS
- Basic API

### Phase 2 — Event-driven

- Domain events
- MediatR handlers

### Phase 3 — Distributed system

- Service split
- RabbitMQ integration
- Consumer implementation

### Phase 4 — Resilience & Observability

- Retry strategies
- Logging
- Monitoring
- Failure handling

---

## 💡 Key Design Decisions

- Start simple, evolve toward distributed architecture
- Prefer explicit architecture over hidden abstractions
- Focus on clarity, maintainability, and demonstrability
- Design for real-world backend scenarios (resilience, async, messaging)

---
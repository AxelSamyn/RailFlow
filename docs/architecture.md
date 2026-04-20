\# RailFlow — Architecture Overview



\## 🎯 Objective



RailFlow is a distributed system demo designed to showcase modern backend architecture skills using .NET, including:



\* Clean Architecture

\* CQRS (Command Query Responsibility Segregation)

\* Vertical Slice Architecture

\* Event-driven communication

\* Distributed system design

\* Observability and resilience patterns



The goal is to demonstrate technical expertise in building scalable, maintainable, and cloud-ready systems.



\---



\## 🚆 Domain Overview



RailFlow simulates a railway logistics system where trains transport cargo between stations.



\### Core Concepts



\* \*\*Train\*\*: Represents a transport unit

\* \*\*Station\*\*: A location where events occur

\* \*\*Cargo\*\*: Goods transported by trains

\* \*\*Segment\*\*: A portion of a journey

\* \*\*Incident\*\*: Unexpected events (delays, failures)



\---



\## 🧩 System Architecture



The system is composed of multiple services:



\### 1. Train Service



\* Source of truth for train lifecycle

\* Handles commands (CreateTrain, UpdateStatus)

\* Emits domain events



\### 2. Tracking Service



\* Builds read models

\* Consumes events from Train Service

\* Provides query endpoints



\### 3. Incident Service



\* Handles anomalies (delays, failures)

\* Reacts to system events

\* Emits incident-related events



\### 4. Web Application



\* Blazor Web App (.NET 8+)

\* Displays system state

\* Sends commands via API



\---



\## 🧱 Architectural Patterns



\### Clean Architecture



\* Separation of concerns

\* Domain-centric design

\* Infrastructure isolated



\### CQRS



\* Commands and Queries are separated

\* Write model optimized for business logic

\* Read model optimized for queries



\### Vertical Slice Architecture



\* Features organized by use case

\* Each slice contains its own logic, validation, and handler



\---



\## 📡 Communication



\### Phase 1 (POC)



\* In-process domain events



\### Phase 2



\* Asynchronous messaging using RabbitMQ

\* Event-driven communication between services



\---



\## 🗄️ Data Storage



\* SQL Server (per service)

\* Each service owns its database

\* No shared database between services



\---



\## 🐳 Infrastructure (Local Development)



Managed via Docker Compose:



\* SQL Server

\* RabbitMQ



Applications run locally during early stages.



\---



\## 📊 Observability (Planned)



\* Logging: Serilog

\* Tracing: OpenTelemetry

\* Metrics: Prometheus / Grafana

\* Correlation IDs for distributed tracing



\---



\## 🔁 Resilience \& Maintenance (Planned)



\* Retry policies (Polly)

\* Dead-letter queues

\* Event replay capability

\* Read model rebuilding



\---



\## 🧪 Testing Strategy



\* Unit tests (Domain)

\* Integration tests (API)

\* Event-driven tests

\* Contract testing between services



\---



\## 🚀 Deployment Strategy (Planned)



\* Dockerized services

\* CI/CD pipeline (GitHub Actions)

\* Optional Azure deployment:



&#x20; \* Azure Service Bus

&#x20; \* Azure Container Apps

&#x20; \* Azure SQL



\---



\## 🧭 Development Roadmap



\### Phase 1 — Monolith foundation



\* Clean Architecture

\* CQRS

\* Basic API



\### Phase 2 — Event-driven



\* Domain events

\* Event handlers



\### Phase 3 — Distributed system



\* Service split

\* RabbitMQ integration



\### Phase 4 — Observability \& resilience



\* Logging

\* Monitoring

\* Failure handling



\---



\## 💡 Key Design Decisions



\* Start simple, evolve to distributed

\* Prefer explicit architecture over abstractions

\* Focus on clarity and maintainability

\* Optimize for demonstration and learning



\---




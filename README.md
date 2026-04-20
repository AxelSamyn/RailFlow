# 🚆 RailFlow

RailFlow is a distributed system demo built with .NET to showcase modern backend architecture and cloud-ready design.

## 🎯 Purpose

This project demonstrates:

* Clean Architecture
* CQRS & Vertical Slice Architecture
* Event-driven communication
* Distributed system design
* Observability and resilience patterns

It is designed as a **technical portfolio project** to highlight senior-level backend and system design skills.

---

## 🧩 Overview

RailFlow simulates a railway logistics system where trains transport cargo between stations.

The system processes events such as:

* Train creation
* Station arrivals
* Delays and incidents

---

## 🏗️ Architecture

The system is composed of multiple services:

* **Train Service** — manages train lifecycle (source of truth)
* **Tracking Service** — builds read models
* **Incident Service** — handles anomalies
* **Web App (Blazor)** — user interface

More details 👉 see `/docs/architecture.md`

---

## ⚙️ Tech Stack

* .NET 8+
* Blazor Web App
* MediatR
* SQL Server
* RabbitMQ
* Docker & Docker Compose

---

## 🚀 Getting Started

### 1. Prerequisites

* Docker Desktop
* .NET SDK
* Visual Studio 2022/2026

---

### 2. Start infrastructure

```bash
docker compose up -d
```

---

### 3. Run the application

(To be completed as the project evolves)

---

## 📊 Roadmap

* [ ] Monolith foundation (Clean Architecture + CQRS)
* [ ] Domain events
* [ ] Distributed services
* [ ] Message broker integration
* [ ] Observability
* [ ] Cloud deployment (Azure)

---

## 🧠 Design Philosophy

* Start simple, evolve to distributed
* Prefer explicit architecture over hidden abstractions
* Focus on clarity, maintainability, and real-world relevance

---

## 📌 Notes

This project is intentionally built step-by-step to demonstrate architectural evolution rather than starting with a fully complex system.

---

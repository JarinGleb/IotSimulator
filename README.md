# IoT Simulation System

A modular IoT backend and device simulation platform built with .NET Core using Clean Architecture principles, Entity Framework Core, and SignalR for real-time telemetry streaming.

## System Architecture

The solution is divided into two primary components: a multi-threaded device simulator and a structured backend server.

### Layers & Structure
* **Domain**: Contains core domain entities (`TelemetryRecord`) independent of external frameworks or databases.
* **Application**: Defines business logic contracts (`ITelemetryRepository`) and orchestrates workflows (`TelemetryService`).
* **Infrastructure**: Implements data access using Entity Framework Core with SQLite (`AppDbContext`, `EfTelemetryRepository`).
* **Presentation**: Manages routing, handles network serialization via Minimal APIs, and exposes the SignalR data hub.

---

## Technical Stack

* **Backend Framework**: ASP.NET Core 8.0 / 9.0 (Minimal APIs)
* **Real-time Transport**: Microsoft ASP.NET Core SignalR
* **ORM**: Entity Framework Core
* **Database**: SQLite (File-based)

---

## Configuration & Installation

### Prerequisites
* .NET SDK (version 8.0 or higher)

### Repository Setup
Clone the repository to your local machine:
```bash
git clone https://github.com/JarinGleb/IotSimulator.git
```

### Database Initialization
The SQLite database file (`TelemetryRecords.db`) is generated automatically on the first application startup via `Database.EnsureCreated()`. No manual database provisioning is required.

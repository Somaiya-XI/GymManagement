# Gym Management
A backend system for managing gym memberships and packages, built with ASP.NET Core 8 using Clean Architecture and Domain-Driven Design principles. Designed for real-world scalability, modularity, and clean separation of concerns.

## Architecture
This project follows Clean Architecture principles with a clear separation of concerns across four layers:

- Domain – Core business rules and entities.
- Application – Use cases and business logic (CQRS-based).
- Infrastructure – External concerns like database access.
- Presentation – API endpoints (controllers & Requests Contracts).

## Tech Stack & Patterns
- ASP.NET Core 9 & Entity Framework
- MediatR – Implements the Mediator pattern for request/response pipelines.
- CQRS (Command Query Responsibility Segregation)
- Repository Pattern
- Unit of Work Pattern
- Result Pattern for clean error handling and responses

## Project Structure
```
GymManagement/
├── GymManagement.API
├── GymManagement.Contracts
├── GymManagement.Application
├── GymManagement.Infrastructure
└── GymManagement.Domain
```

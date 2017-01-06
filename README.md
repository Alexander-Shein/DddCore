# Domain Driven Design implementation for .net core 

Setup:



## Layred arhitecture:

- Crosscutting

- Business Logic Layer (BLL)

- Data Access Layer (DAL)

- Services Layer (SL)

- Presentation Layer (PL)

## Features:

### Crosscutting

- [Dependency injection][1]
- Object mapper

### BLL

- Entity

- Aggregate root entity

- Business rules

- Domain events

### DAL

- Repository

- Query repository

- Unit of Work

### SL

- Entity services

- Workflow services

- Infrastructure services

### PL

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/DependencyInjection/README.md

# Domain Driven Design arhitecture implementation for .net core 

Setup:

## Features:

### Crosscutting

- [Dependency injection][1]
- [Object mapper][2]

### Business Logic Layer (BLL)

- [Entity][4]

- [Aggregate root entity][4]

- [Business rules][5]

- Domain events and handlers

### Data Access Layer (DAL)

- [Repository][3]

- [Unit of Work][3]

- Query repository

- Entity mapping

### Services Layer (SL)

- Entity services

- Workflow services

- Infrastructure services

### PL

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/DependencyInjection/README.md
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/ObjectMapper/README.md
[3]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/DddCore.Contracts.Dal/DomainStack/README.md
[4]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/DddCore.Contracts.Domain/Entities/README.md
[5]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/DddCore.Contracts.Domain/Entities/BusinessRules/README.md

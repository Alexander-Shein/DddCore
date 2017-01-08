# Domain Driven Design arhitecture implementation for .net core 

Setup:

## Features:

### Crosscutting

- [Dependency injection][1]
- [Object mapper][2]

### Business Logic Layer (BLL)

- [Entity][3]

- [Aggregate root entity][4]

- [Business rules][5]

- [Domain events and handlers][6]

- Value object

### Data Access Layer (DAL)

- [Repository][7]

- [Unit of Work][8]

- [Query repository][9]

- [Entity mapping][10]

### Services Layer (SL)

- Entity services

- Workflow services

- Infrastructure services

### PL

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/DependencyInjection/README.md
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/ObjectMapper/README.md

[3]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md
[4]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#aggregate-root-entity
[5]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#business-rules
[6]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#domain-events-and-handlers

[7]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#repository
[8]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#unit-of-work
[9]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#queryrepository
[10]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#entity-mapping

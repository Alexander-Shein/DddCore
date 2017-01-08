# Domain Driven Design arhitecture implementation for .net core 

Setup:

## Features:

### Crosscutting

- [Dependency injection][1]
- [Object mapper][2]

### Business Logic Layer (BLL)

- [Entity][3]

- [Aggregate Root Entity][4]

- [Business Rules][5]

- [Domain events and handlers][6]

- [Value Object][7]

### Data Access Layer (DAL)

- [Repository][8]

- [Unit of Work][9]

- [Query Repository][10]

- [Entity Mapping][11]

### Services Layer (SL)

- [Entity Service][12]

- [Workflow Service][13]

- [Infrastructure Service][14]

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/DependencyInjection/README.md
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/DddCore.Contracts.Crosscutting/ObjectMapper/README.md

[3]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md
[4]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#aggregate-root-entity
[5]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#business-rules
[6]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#domain-events-and-handlers
[7]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#value-object

[8]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#repository
[9]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#unit-of-work
[10]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#queryrepository
[11]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#entity-mapping

[12]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#entity-service
[13]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#workflow-service
[14]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#infrastructure-service

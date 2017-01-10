# Domain Driven Design (DDD) arhitecture implementation for .net core
DddCore framework implements common ddd patterns like repository, unit of work, aggregate roots, domain events, query repositories, entity services and others. The goal of this framework is do not implement ddd patterns for every project from scratch and have well documented, fully tested components that you need for your new projects. The framework is easy to use with less code. DddCore uses layered arhitecture. Below you can find all DddCore features and samples:

DddCore framework uses next libraries:
* [FluentAssertion][-4]
* [EntityFramework][-3]
* [Microsoft DependencyInjection][-2] - can be switched
* [AutoMapper][-1] - can be switched

[Sample][0]

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

- [Connection Strings][12]

### Services Layer (SL)

- [Entity Service][13]

- [Workflow Service][14]

- [Infrastructure Service][15]

[-4]: https://github.com/JeremySkinner/FluentValidation
[-3]: https://github.com/aspnet/EntityFramework
[-2]: https://github.com/aspnet/DependencyInjection
[-1]: https://github.com/AutoMapper/AutoMapper

[0]: https://github.com/Alexander-Shein/DddCore/tree/net-core/Samples/Api
[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/README.md#dependency-injection
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/README.md#object-mapper

[3]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md
[4]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#aggregate-root-entity
[5]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#business-rules
[6]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#domain-events-and-handlers
[7]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#value-object

[8]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#repository
[9]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#unit-of-work
[10]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#query-repository
[11]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#entity-mapping
[12]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/DAL/README.md#connection-strings

[13]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#entity-service
[14]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#workflow-service
[15]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#infrastructure-service

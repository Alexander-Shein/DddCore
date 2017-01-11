# Domain Driven Design (DDD) arhitecture implementation for .net core
The goal of this framework is do not implement ddd patterns for every project from scratch and have well documented, fully tested and easy to use components that you need for your new projects.

* DddCore framework implements common DDD patterns like repository and query repository, unit of work, aggregate roots, domain events, entity services and others.

* Diffrent ORMs: functional EntityFramework for CRUD operations (repository) and fast Dapper for read operations (query repository). It's in place because of perfomance and to avoid redundant entity relationships that are added in order to create a linq query to return data to UI. As a result better domain models structure.

* Layed arhitecture: Crosscutting, Business Logic Layer, Data Access Layer, Services Layer and Presentation Layer.

* DddCore is automatted as much as possible. All components are auto registered to dependency injection container. Generic implementations are provided for components like repository and entity services. Common entity fields are auto mapped for Entity Framework.

* DddCore has async and sync imlementations for all methods.

* DddCore framework uses next libraries: [FluentAssertion][-4], [EntityFramework][-3], [Dapper][-5], [Microsoft Dependency Injection][-2], [AutoMapper][-1](can be switched)

Try a tutorial(under construction) or have a look to the [sample][0] app.

## Features:

### Crosscutting

- [Dependency Injection][1]
- [Object Mapper][2]
- [User Context][-7]
- [Extension Methods and Helpers][-6]

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
- [Pagged Result][16]
- Crud Interfaces

[-4]: https://github.com/JeremySkinner/FluentValidation
[-3]: https://github.com/aspnet/EntityFramework
[-2]: https://github.com/aspnet/DependencyInjection
[-1]: https://github.com/AutoMapper/AutoMapper
[-5]: https://github.com/StackExchange/dapper-dot-net

[0]: https://github.com/Alexander-Shein/DddCore/tree/net-core/Samples/Api
[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/README.md#dependency-injection
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/README.md#object-mapper
[-7]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/README.md#user-context
[-6]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/Crosscutting/README.md#object-mapper#extension-methods-and-helpers

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
[16]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/SL/README.md#pagged-result

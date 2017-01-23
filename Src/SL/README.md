# Entity Service

## Dependency injection
For each Aggregate Root the generic implementation of IEntityService<> is auto registered and can be injected via generic interface. Lifestyle is Scoped.

## Overview
When business logic requires interaction with different layers or it needs to use services but we can't inject it to entity this logic goes to entity services. For example if we need to interact with a repository this implementation goes to entity services.

Generic Entity Service:
```csharp
public interface IEntityService<in T, in TKey> where T : class, IAggregateRootEntity<TKey>
{
    /// <summary>
    /// Validate business rules, raise events and persist aggregate root graph.
    /// Throws first broken business rule if any.
    /// </summary>
    /// <param name="aggregateRoot"></param>
    /// <returns></returns>
    void PersistAggregateRoot(T aggregateRoot);

    /// <summary>
    /// Async version of PersistAggregateRoot.
    /// </summary>
    /// <param name="aggregateRoot"></param>
    /// <returns></returns>
    Task PersistAggregateRootAsync(T aggregateRoot);

    /// <summary>
    /// Validate business rules, raise events and persist aggregate root graph.
    /// </summary>
    /// <param name="aggregateRoot"></param>
    /// <returns>if BusinessRulesValidationResult.IsValid then aggregate root is persisted. If not BusinessRulesValidationResult.BrokenBusinessRules is populated.</returns>
    BusinessRulesValidationResult TryPersistAggregateRoot(T aggregateRoot);

    /// <summary>
    /// Async version of TryPersistAggregateRoot.
    /// </summary>
    /// <param name="aggregateRoot"></param>
    /// <returns></returns>
    Task<BusinessRulesValidationResult> TryPersistAggregateRootAsync(T aggregateRoot);
}
```

## Custom implementation
Interface for custom entity service implementation should be derived from generic IEntityService<> with related generic types:
```csharp
public interface ICarsEntityService : IEntityService<Car, Guid>
{
    void AddAirBag(AirBag airBag);
}
```
The implementation should be derived from generic implementation EntityService<>. Generic implementation has all methods marked as virtual so it could be overriten if required: 
```csharp
public class CarsEntityService : EntityService<Car, Guid>, ICarsEntityService
{
    public CarsEntityService(IRepository<T, TKey> repository, IGuard guard, IDomainEventDispatcher domainEventDispatcher) : base(repository, guard, domainEventDispatcher) { ... }

    public void AddAirBag(AirBag airBag) { ... }
    
    public void override PersistAggregateRoot(T aggregateRoot) { ... }
}
```
If custom implementation exists then for IEntityService<> generic interface will be injected custom implementation as well as for custom interface:
```csharp
public class CarsWorkflowService : ICarsWorkflowService
{
    public CarsWorkflowService(IEntityService<Car, Guid> carsEntityService) { ... }
}
```
Or
```csharp
public class CarsWorkflowService : ICarsWorkflowService
{
    public CarsWorkflowServiceICarsEntityService carsEntityService) { ... }
}
```

# Workflow Service

## Dependency injection:
Services marked as IWorkflowService are auto registered with scoped lifestyle.

## Overview
The responsibily of those services is a workflow and transaction control. Those services contain NOT reusable logic because each workflow should have only one enter point. IUnitOfWork should be injected only into workflow service.

Example:
```csharp
public interface ICarsWorkflowService : IWorkflowService
{
    Task<CarVm> CreateCarAsync(CarIm im);
}
```
```csharp
public class CarsWorkflowService : ICarsWorkflowService
{
    readonly IUnitOfWork unitOfWork;
    
    public CarsWorkflowService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<CarVm> CreateCarAsync(CarIm im)
    {
        // Insert new car
        
        unitOfWork.Save();
        
        return carVm;
    }
}
```

Note: Not reusable logic

Note: Transactions control

Note: Workflow control


# Infrastructure Service

## Dependency injection:
Services marked as IInfrastructureService are auto registered with scoped lifestyle.

## Overview
It's a place for reusable logic that is not part of business logic and workflow logic. Because business logic goes to [entities][1] and entity services. Workflow logic goes to workflow services. Transactions (using IUnitOfWork) is not allowed because those services contain reusable logic that can be used in many places and it's possible to have multiple transactions per one requiest. 

Usage examples:
* Facades for third party APIs
* Internal services that are used by workflow/entity services
* Helper services
* other logic

Examples from framework that can be used in your code:

```csharp
public interface IGuard : IInfrastructureService
{
    /// <summary>
    /// If null throws AgrumentNullException.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="message"></param>
    void NotNull(object obj, string message = "");

    /// <summary>
    /// Retrives business rules validator and validates business rules for aggregateRoot.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="aggregateRoot"></param>
    /// <returns>Business rules validation result</returns>
    BusinessRulesValidationResult ValidateBusinessRules<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;

    /// <summary>
    /// Async version of ValidateBusinessRules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="aggregateRoot"></param>
    /// <returns></returns>
    Task<BusinessRulesValidationResult> ValidateBusinessRulesAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
}
```
```csharp
public interface IPaggingService : IInfrastructureService
{
    /// <summary>
    /// If <param name="page"></param> less than <param name="firstPageNumber"></param> returns <param name="firstPageNumber"></param>.
    /// Else returns <param name="page"></param>
    /// </summary>
    /// <param name="page"></param>
    /// <param name="firstPageNumber"></param>
    /// <returns></returns>
    int NormalizePage(int page, int firstPageNumber = 1);

    /// <summary>
    /// If <param name="pageSize"></param> less than <param name="minPageSize"></param> returns <param name="minPageSize"></param>.
    /// Else if <param name="pageSize"></param> greater than <param name="maxPageSize"></param> returns <param name="maxPageSize"></param>.
    /// Else returns <param name="pageSize"></param>.
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="minPageSize"></param>
    /// <param name="maxPageSize"></param>
    /// <returns></returns>
    int NormalizePageSize(int pageSize, int minPageSize = 10, int maxPageSize = 1000);
}
```

Note: IUnitOfWork (transactions) is not allowed

Note: Business logic is not allowed

Note: Workflow logic is not allowed

# Pagged Result

DddCore has a model that can be helpful for pagged result:
```csharp
public class PaggedResult<T>
{
    public PaggedResult(int page, int pageSize, IEnumerable<T> items, long total)
    {
        Page = page;
        PageSize = pageSize;
        Items = items;
        Total = total;
    }

    public long Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public IEnumerable<T> Items { get; set; }
}
```

# Interfaces for Crud Operations

DddCore has a set of predefined interfaces for crud operations. It could be helpful to build your contracts. It has a async and sync versions.

### Note:
Input models don't contain Id property. Because in the Create(POST) operation we don't know an id. It will be generated on the server in the call. And for the Update(PUT) operation we don't need id property because the id is provided in the URL.
Exampe: PUT /cars/{id}. The id is provided in URL and in the body we have a InputModel without id.

### Note: 
When you need to create an item in child collection you can use ICreateChild interface. But we don't need IUpdateChild/IDeleteChild/IReadChild because we already have an id and can use next short urls without /cars/{carId} prefix: GET/DELETE/PUT /wheels/55.

### Note:
DddCore has async equvalents for CRUD interfaces. They have an Async prefix: ICreateAsync, IReadAsync, IUpdateAsync, IDeleteAsync, ICrudAsync and ICreateChildAsync. If you need async version just add Async prefix to examples below.

Sync CRUD version:
```csharp
public interface ICreate<out TViewModel, in TInputModel>
{
    /// <summary>
    /// Example: POST /cars/ HTTP/1.1.
    /// Creates a new entity by InputModel.
    /// </summary>
    /// <param name="im">InputModel has no Id property because when we send request to create new object we don't know id.</param>
    /// <returns>ViewModel contains generated Id property.</returns>
    TViewModel Create(TInputModel im);
}
```
```csharp
public interface IRead<out TViewModel, in TKey>
{
    /// <summary>
    /// GET /cars/{carId} HTTP/1.1.
    /// Reads entity by key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="includes">Includes can contain additional information that we need to return.</param>
    /// <returns></returns>
    TViewModel Read(TKey key, string[] includes = null);
}
```
```csharp
public interface IUpdate<out TViewModel, in TKey, in TInputModel>
{
    /// <summary>
    /// PUT /cars/{carId}/ HTTP/1.1.
    /// Updates by key or creates new entity with specified id and returns ViewModel.
    /// </summary>
    /// <param name="key">Id of entity that will be updated.</param>
    /// <param name="im">InputModel has no Id property because when we send request to update new object we pass id to url.</param>
    /// <returns>ViewModel contains Id property.</returns>
    TViewModel CreateOrUpdate(TKey key, TInputModel im);
}
```
```csharp
public interface IDelete<in TKey>
{
    /// <summary>
    /// DELETE /cars/{carId} HTTP/1.1.
    /// Deletes entity by key.
    /// </summary>
    /// <param name="key"></param>
    void Delete(TKey key);
}
```
```csharp
public interface ICreateChild<out TViewModel, in TParrentKey, in TInputModel>
{
    /// <summary>
    /// Example: POST /cars/{carId}/wheels HTTP/1.1.
    /// When you need to create an item in child collection you can use this interface.
    /// But we don't need UpdateChild/DeleteChild/ReadChild because we already have an id and can use next short urls without /cars/{carId} prefix: GET/DELETE/PUT /wheels/55.
    /// </summary>
    /// <param name="key">This is a parrent item key</param>
    /// <param name="im">InputModel has no Id property because when we send request to create new object we don't know id.</param>
    /// <returns>ViewModel contains generated Id property.</returns>
    TViewModel CreateChild(TParrentKey key, TInputModel im);
}
```

ICrud interface contains all CRUD interfaces if you need all set of methods:
```csharp
public interface ICrud<out TVm, in TKey, in TIm> :
    ICreate<TVm, TIm>,
    IRead<TVm, TKey>,
    IUpdate<TVm, TKey, TIm>,
    IDelete<TKey>
    where TIm : class
    where TVm : class
{
}
```

[Return][2]

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#entity
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/README.md

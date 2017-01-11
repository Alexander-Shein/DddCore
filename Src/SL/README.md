# Entity Service

## Dependency injection
For each Aggregate Root the generic implementation of IEntityService<> is auto registered and can be injected via generic interface. Lifestyle is Scoped.

## Overview
When business logic requires interaction with diffrent layers or it needs to use services but we can't inject it to entity this logic goes to entity services. For example if we need to interact with a repository this implementation goes to entity services.

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
    public CarsEntityService(IRepository<T, TKey> repository, IGuard guard, IDomainEventDispatcher domainEventDispatcher) : base(repository, guard, domainEventDispatcher)
{
}

public void AddAirBag(AirBag airBag) { ... }
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
Services marked as IWorkflowService are auto registered with PerWebRequest lifestyle.

## Overview
The responsibily of those services is a workflow and transaction control. Those services contain NOT reusable logic because each workflow should have only one enter point. IUnitOfWork should be injected only into workflow service.

Example:
```csharp
public interface ICarsWorkflowService : IWorkflowService
{
    Task<CarVm> CreateCarAsync(CarIm im);
}

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
Services marked as IInfrastructureService are auto registered with PerWebRequest lifestyle.

## Overview
It's a place for reusable logic that is not part of business logic and workflow logic. Because business logic goes to [entities][1] and entity services. Workflow logic goes to workflow services. Transactions (using IUnitOfWork) is not allowed because those services contain reusable logic that can be used in many places and it's possible to have multiple transactions per one requiest. 

Usage examples:
* Facades for third party APIs
* Internal services that are used by workflow/entity services
* Helper services
* other logic

Examples from framework:

```csharp
public interface IGuard : IInfrastructureService
{
    void NotNull(object obj, string message = "");
    Task AggregateRootIsValidAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
    void AggregateRootIsValid<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
}
```
And
```csharp
public interface IPaggingService : IInfrastructureService
{
    int MormalizePage(int page);
    int NormalizePageSize(int pageSize);
}
```

Note: IUnitOfWork (transactions) is not allowed

Note: Business logic is not allowed

Note: Workflow logic is not allowed

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/Src/BLL/README.md#entity

# Entity Service

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

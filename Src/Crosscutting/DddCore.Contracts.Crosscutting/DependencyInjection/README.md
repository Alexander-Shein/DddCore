# Dependency injection

## Bootstrap

  ```csharp
new DiBootstrapper()
    .AddMicrosoftDependencyInjection(services)    
    .Bootstrap();
```

## Modules
Bootstrap method scans all assemblies for IDiModule implementation and passes IContainerConfig to Install method. If you need to register something to container just create a module and it be installed:
```csharp
public class DddCoreDiModule : IDiModule
{
    public void Install(IContainerConfig config)
    {
        config
            .Register<IUnitOfWork, DataContext>()
            .LifeStyle
            .PerWebRequest();

        config
            .Register<IDataContext, DataContext>()
            .LifeStyle
            .PerWebRequest();

        config
            .Register<IDomainEventDispatcher, DomainEventDispatcher>()
            .LifeStyle
            .PerWebRequest();
    }
}
```

## Inject container
If you want a container instance you can use IServiceProvider interface to inject it:
```csharp
public class BusinessRulesValidatorFactory : IBusinessRulesValidatorFactory
{
    public BusinessRulesValidatorFactory(IServiceProvider serviceProvider)
    {
    }
}
```

## Auto registration
Next components from framework have auto registration. You don't need to register it mannualy.

- BusinessRulesValidator

```csharp
public class CarBusinessRulesValidator : BusinessRulesValidatorBase<Car>
{
    public CarBusinessRulesValidator()
    {
        RuleFor(x => x.Color)
            .Length(0, 50);
    }
}
```

-  Custom and generic repository

```csharp
public interface ICarsRepository : IRepository<Car, Guid>
public class CarsRepository : ICarsRepository // It's automatically registered for ICarsRepository and IRepository<Car, Guid>
```

If no custom repository provided for aggregate root the system registers a generic repository automatically for this aggregate root and can be resolved by generic repository interface:
```csharp
IRepository<Car, Guid>
```

- QueryRepository

```csharp
public interface ICarsQueryRepository : IQueryRepository
public class CarsQueryRepository : ICarsQueryRepository // It's automatically registered for ICarsQueryRepository
```

- Custom and generic Entity services

- Infrastructure services

- Workflow services

- Domain event handlers






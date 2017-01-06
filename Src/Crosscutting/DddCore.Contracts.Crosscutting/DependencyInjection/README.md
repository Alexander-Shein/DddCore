# Dependency injection

## Bootstrap

  ```csharp
new DiBootstrapper()
    .AddMicrosoftDependencyInjection(services)    
    .Bootstrap();
```

## Modules
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
```csharp
public class BusinessRulesValidatorFactory : IBusinessRulesValidatorFactory
{
    readonly IServiceProvider serviceProvider;

    public BusinessRulesValidatorFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
}
```

## Auto registration

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

- Repository

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

- Entity services

- Infrastructure services

- Workflow services

- Domain event handlers






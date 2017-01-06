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

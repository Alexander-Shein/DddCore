# Dependency injection

## Bootstrap

Bootstrap method automatically register all framework components and custom components that are implements framework interfaces or inherited from framework base classes. In theory you don't need to register anything mannualy to the container. But if you need see the Modules section. Bootstrap example:

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

# Object mapper

## Bootstrap

```csharp
var objectMapper = new ObjectMapperBootstrapper()
    .AddAutoMapperConfig()
    .Bootstrap();
```

## Modules

```csharp
public class ObjectMapperModule : IObjectMapperModule
{
    #region Public Methods

    public void Install(IObjectMapperConfig config)
    {
        config.Bind<ObjectFrom, ObjectTo>(bindingConfig =>
        {
            bindingConfig.Bind(x => x.Property1, x => x.Property2);
            bindingConfig.Bind(x => x.Property2, x => x.Property1);
            bindingConfig.Ignore(x => x.Property3);
        });
    }

    #endregion
}
```

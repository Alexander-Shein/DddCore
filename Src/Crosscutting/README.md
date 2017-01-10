# Dependency injection

## Bootstrap

To register all framework components and custom components that are implements framework interfaces or inherited from framework base classes use .AddDddCore() method;

  ```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();
    services.AddDddCore();
}
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

Bootstrap methos scans all assemblies for IObjectMapperModule and executes Install method. If you need to add mappings use IObjectMapperModule. Bootstrap method:

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

# Dependency injection

## Bootstrap
Use .AddDddCore extension method to add all framework components. Example:

  ```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();
    services.AddDddCore();
}
```

## Auto registration
If class implements one of the framework interface such as IQueryRepository, IWorkflowService, IRepository... it is gonna be auto registered. Mannual registration is not needed.

## Modules
If mannual registration is needed IDiModuleInstaller can be used. services.AddDddCore() method scans all assemblies for IDiModuleInstaller modules and passes IServiceCollection to Install method. If you need to register something to container just create a module and register components in the Install method:
### Note:
IDiModule shoud have parameterless constructor.

```csharp
public class DddCoreDiModule : IDiModule
{
    public void Install(IServiceCollection serviceCollection)
    {
            serviceCollection.AddScoped<IUnitOfWork, DataContext>();
            serviceCollection.AddScoped<IDataContext, DataContext>();
            serviceCollection.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}
```
Or use standsrt ```csharp void ConfigureServices(IServiceCollection services) ``` method. 

# Object mapper
## Bootstrap

To setup object mapper use bootstrap method from ObjectMapperBootstrapper class which scans all assemblies for IObjectMapperModule and executes Install method. If you need to add mappings use IObjectMapperModule. Example:

```csharp
  var objectMapper = new ObjectMapperBootstrapper()
      .AddAutoMapperConfig()
      .Bootstrap();

  services.AddSingleton(objectMapper);
```

## Modules
Bootstrap method scans all assemblies for IObjectMapperModule implementation and invokes install method. In the module you can setup mapping. Example:
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
##IObjectMapper usage
Inject IObjectMapper instance and use map method for mapping objects:
```csharp
public interface IObjectMapper
{
    /// <summary>
    /// Maps object from passed to new object with generic type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="from"></param>
    /// <returns></returns>
    T Map<T>(object from);
}
```

# User Context

DddCore has a wrapper for current IIdentity:

```csharp
public interface IUserContext<out TKey>
{
    /// <summary>
    /// ClaimTypes.NameIdentifier from current Identity
    /// </summary>
    TKey Id { get; }

    /// <summary>
    /// Identity.Name
    /// </summary>
    string UserName { get; }

    /// <summary>
    /// Identity.IsAuthenticated
    /// </summary>
    bool IsAuthenticated { get; }
}
```

The IIdentity is given from IHttpContextAccessor:

```csharp
IHttpContextAccessor
    .HttpContext
    .User
    .Identity
```

And can be injected via IUserContext<> interface.

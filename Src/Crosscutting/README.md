# Dependency injection

## Bootstrap
To register all framework components and custom components use .AddDddCore() method. Example:

  ```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();
    services.AddDddCore();
}
```

## Modules
Bootstrap method scans all assemblies for IDiModule implementation and passes IServiceCollection to Install method. If you need to register something to container just create a module and it will be installed:
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
Or use ConfigureServices(IServiceCollection services) method.

# Object mapper
## Bootstrap

Bootstrap methos scans all assemblies for IObjectMapperModule and executes Install method. If you need to add mappings use IObjectMapperModule. Bootstrap oblect mapper example:

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
##IObjectMapper
Use IObjectMapper instance to map object:
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

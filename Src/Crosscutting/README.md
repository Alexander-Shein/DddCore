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
Bootstrap method scans all assemblies for IDiModule implementation and passes IServiceCollection to Install method. If you need to register something to container just create a module and it be installed:
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
Or use ConfigureServices(IServiceCollection services) method

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

# Extension Methods and Helpers

## Type
```csharp
public static class TypeExtensions
{
    /// <summary>
    /// Type.IsAssignableFrom equivalent for opened generic types
    /// </summary>
    /// <param name="genericType"></param>
    /// <param name="givenType"></param>
    /// <returns></returns>
    public static bool IsAssignableFromGenericType(this Type genericType, Type givenType) { ... }
}
```

## String
```csharp
/// <summary>
/// String.Contains equivalent with <paramref name="comp"/>
/// </summary>
/// <param name="source"></param>
/// <param name="toCheck"></param>
/// <param name="comp"></param>
/// <returns></returns>
public static bool Contains(this string source, string toCheck, StringComparison comp) { ... }
```

## IEnumerable

```csharp
/// <summary>
/// Devide IEnumerable to chunks
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <param name="values"></param>
/// <param name="chunkSize"></param>
/// <returns></returns>
public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(this IEnumerable<TValue> values, int chunkSize) { ... }
```

```csharp
/// <summary>
/// Check if collection is null or empty
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="enumerable"></param>
/// <returns></returns>
public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) { ... }
```

```csharp
/// <summary>
/// Perform action on every element in collection
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="source"></param>
/// <param name="action"></param>
/// <returns></returns>
public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action) { ... }
```

```csharp
/// <summary>
/// Check is collection comtains only one element
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="list"></param>
/// <returns></returns>
public static bool IsSingle<T>(this IEnumerable<T> list) { ... }
```



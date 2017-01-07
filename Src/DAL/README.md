# Repository

Generic repository:

```csharp
public class Repository<T, TKey> : IRepository<T, TKey> where T : class, IAggregateRootEntity<TKey>
{
    public virtual void PersistAggregateRoot(T entity) { ... }

    public virtual async Task<T> ReadAggregateRootAsync(TKey key) { ... }

    protected DbSet<T> GetDbSet() { ... }
}
```

For each aggregate root a generic repository can be injected. Example:

```csharp
public class CarsEntityService : ICarsEntityService
{
  public CarsEntityService(IRepository<Car, Guid> carsRepository) {}
}
```

Create custom repository:

```csharp
public interface ICarsRepository : IRepository<Car, Guid>
{
  IEnumerable<Car> GetCarsWithColor(string color);
}

public class CarsRepository : Repository<Car, Guid>, ICarsRepository
{
  IEnumerable<Car> GetCarsWithColor(string color) { ... }
}
```

Inject custom repository:
```csharp
public class CarsEntityService : ICarsEntityService
{
  public CarsEntityService(ICarsRepository carsRepository) {}
}
```

Note: protected DbSet<T> GetDbSet() can be used in custom repository to interact with EF collection

# Unit of Work

Api:

```csharp
public interface IUnitOfWork
{
    Task SaveAsync();
    void Save();
}
```

Inject UoW example:

```csharp
public class CarsWorkflowService : ICarsWorkflowService
{
  public CarsWorkflowService(IUnitOfWork unitOfWork) {}
}
```

# QueryRepository





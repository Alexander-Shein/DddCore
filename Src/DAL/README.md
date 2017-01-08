# Repository

## Dependency injection
For each Aggregate Root the generic implementation of IRepository<> is auto registered and can be injected. When custom repository for aggregate root is created then generic repository is overritten. Lifestyle is PerWebRequest.

## Overview
Generic repository:

```csharp

public interface IRepository<T, in TKey> where T : class, IAggregateRootEntity<TKey>
{
    void PersistAggregateRoot(T entity);
    Task<T> ReadAggregateRootAsync(TKey key);
}
```
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

Custom repository:

```csharp
public interface ICarsRepository : IRepository<Car, Guid>
{
  IEnumerable<Car> GetCarsWithColor(string color);
}
```
```csharp
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

## Dependency injection
It's auto registered and can be injected via IUnitOfWork interface.

## Overview

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

# Query Repository

## Dependency injection
Services marked as IQueryRepository are auto registered with PerWebRequest lifestyle.

## Overview
[Dapper][1] is used for query repository

Query repository example:
```csharp
public class CarVmDto
{
    public string PublicKey { get; set; }
    public string Color { get; set; }
}
```
```csharp
public interface ICarsQueryRepository : IQueryRepository
{
    Task<IEnumerable<CarVmDto>> GetAllCarsAsync();
}
```
```csharp
public class CarsQueryRepository : QueryRepositoryBase, ICarsQueryRepository
{
    public CarsQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
    {
    }

    public async Task<IEnumerable<CarVmDto>> GetAllCarsAsync()
    {
        var sql = "SELECT * FROM [dbo].[Car];";

        var dtos = await GetFilteredListAsync<CarVmDto>(sql);
        return dtos;
    }
}
```

# Entity Mapping

[EntityFramework fluent api][3] is used for mapping.
Framework automatically loads all intances of IMappingModule interface and passes EntityFramework ModelBuilder. Example:

```csharp
public class CarsMappingModule : IMappingModule
{
    public void Install(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Wheel>()
            .Ignore(x => x.CrudState)
            .HasKey(x => x.Id)
            .ForSqlServerIsClustered(); ;

        var carEntityBuilder = modelBuilder.Entity<Car>();

        carEntityBuilder
            .HasMany(x => x.Wheels)
            .WithOne()
            .HasForeignKey(x => x.CarId);

        carEntityBuilder
            .Ignore(x => x.CrudState)
            .Property(x => x.Ts)
            .IsRowVersion();

        carEntityBuilder
            .HasKey(x => x.Id)
            .ForSqlServerIsClustered();
    }
}
```

[Return][2]

[1]: https://github.com/StackExchange/dapper-dot-net
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/README.md
[3]: https://msdn.microsoft.com/en-us/library/jj591617(v=vs.113).aspx



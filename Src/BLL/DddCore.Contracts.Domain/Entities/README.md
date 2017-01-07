# Entities and Aggregate root entities

Base class for entities:

```csharp
public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    public CrudState CrudState { get; set; }
    public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();
}
```
* Id - unique entity identifier
* CrudState - Unchanged/Added/Modified/Deleted state will be used in repository to sync entity with database 
* Events - events that will be automaticaly handled by domain event handlers

```csharp
public class Wheel : EntityBase<Guid>{...}
```

Base class for aggregate root entities:

```csharp
public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
{
    public string PublicKey { get; set; }
    public byte[] Ts { get; set; }

    public void WalkEntireGraph(Action<IEntity<TKey>> action) { ... }
    public void WalkAggregateRootGraph(Action<IEntity<TKey>> action) { ... }
}
```

* PublicKey - user readable public key for urls
* Ts - row version for optimistic concurrency
* WalkEntireGraph - walk throw all entities in the graph
* WalkAggregateRootGraph - walk throw all entities in current aggregate root

```csharp
public class Car : AggregateRootEntityBase<Guid>{...}
```

[Return][1]

[1]: https://github.com/Alexander-Shein/DddCore/blob/net-core/README.md

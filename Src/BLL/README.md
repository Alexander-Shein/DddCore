# Entity

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

Example:
```csharp
public class Wheel : EntityBase<Guid>{...}
```

# Aggregate root entity

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

Example:
```csharp
public class Car : AggregateRootEntityBase<Guid>{...}
```

# Business rules

For business rules [FluentValidation][1] is used

Base class:

```csharp
public abstract class BusinessRulesValidatorBase<T> : AbstractValidator<T>, IBusinessRulesValidator<T> where T : ICrudState
{
    public async Task<BusinessRulesValidationResult> ValidateAsync(T instance) { ... }
    public new BusinessRulesValidationResult Validate(T instance) { ... }
}
```

Example:
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

# Domain events and handlers

Domain event example:
```csharp
public class ColorChangedDomainEvent : IDomainEvent
{
    public ColorChangedDomainEvent(Car car)
    {
        Car = car;
    }

    public Car Car { get; }

    public DateTime CreatedAt { get; set; }
}
```

Domain event handler example:
```csharp
public class UpdateColorHandler : IDomainEventHandler<ColorChangedDomainEvent>
{
    public void Handle(ColorChangedDomainEvent args)
    {
        args.Car.Color += "-Updated";
    }
}
```

Adding domain event example:
```csharp
public class Car : AggregateRootEntityBase<Guid>
{
    public string Color { get; set; }

    public void UpdateColor(string color)
    {
        Events.Add(new ColorChangedDomainEvent(this));
        Color = color;
    }
}
```

[Return][2]

[1]: https://github.com/JeremySkinner/FluentValidation
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/README.md

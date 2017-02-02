# Entity

Each entity should be derived from EntityBase<> base class:

```csharp
public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    public CrudState CrudState { get; set; }
    public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();
}
```
* Id - int/Guid/long unique entity identifier (primary key) 
* CrudState - Unchanged/Added/Modified/Deleted state will be used in repository to sync entity with database. For example if you want to update the entity in the DataBase just set CrudState to Modified
* Events - events that will be automaticaly handled by domain event handlers. Just insert a domain event to this collection and the event will be raised when you persist entity
 
Example:
```csharp
public class Wheel : EntityBase<Guid>{...}
```

# Aggregate root entity

Base class for aggregate root entities:

```csharp
public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
{
    public void WalkEntireGraph(Action<IEntity<TKey>> action) { ... }
    public void WalkAggregateRootGraph(Action<IEntity<TKey>> action) { ... }
}
```

* WalkEntireGraph - walk throught all entities in the graph
* WalkAggregateRootGraph - walk throught all entities in current aggregate root

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

### Note:
For more validation examples see [FluentValidation][1]

### Note:
When aggregate root is persisted via IEntityService.PersistEntityGraph the related BusinessRulesValidator is invoked against aggregate root entity and validation is performed.

# Domain events and handlers

## Dependency injection
Handlers marked with IDomainEventHandler<> interface are auto registered with Scoped lifestyle.

## Overview
All domain events should implement IDomainEvent interface:
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

Each entity contains property Events collection. In order to raise an event you just need to add your domain event to this property:
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

## Domain event dispatcher

If you want to raise events mannualy you can inject and use IDomainEventDispatcher.Raise:
```csharp
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Pass domain event to related handlers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="domainEvent"></param>
    void Raise<T>(T domainEvent) where T : IDomainEvent;
}
```

### Note:
When aggregate root is persisted via IEntityService.PersistEntityGraph the events from all aggregate root graph entities are raised and passed to related event handlers.

# Value Object

For the details check this [link][3]

[Return][2]

[1]: https://github.com/JeremySkinner/FluentValidation
[2]: https://github.com/Alexander-Shein/DddCore/blob/net-core/README.md
[3]: http://grabbagoft.blogspot.com/2007/06/generic-value-object-equality.html

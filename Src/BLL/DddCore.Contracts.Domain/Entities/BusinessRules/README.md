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


[1]: https://github.com/JeremySkinner/FluentValidation

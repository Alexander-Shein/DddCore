using System;
using System.Collections.Generic;
using System.Linq;
using DddCore.Contracts.BLL.Domain.BusinessRules;
using DddCore.Contracts.BLL.Domain.Services;
using FluentValidation;
using FluentValidation.Results;
using Severity = FluentValidation.Severity;

namespace DddCore.BLL.Domain.Models.BusinessRules
{
    public abstract class BusinessRulesValidatorBase<T> : AbstractValidator<T>, IBusinessRulesValidator<T>
    {
        public const string Data = "ValidationData";

        #region Public Methods

        public new Result Validate(T instance)
        {
            var validationResult = base.Validate(instance);
            return Map(validationResult);
        }

        public Result Validate<TData>(T instance, TData data)
        {
            var context = new ValidationContext<T>(instance)
            {
                RootContextData = {[Data] = data}
            };

            var validationResult = base.Validate(context);
            return Map(validationResult);
        }

        public Result Validate(T instance, string ruleSetName)
        {
            var validationResult = base.Validate(instance);
            return Map(validationResult);
        }

        public Result Validate<TData>(T instance, string ruleSetName, TData data)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var instance = context.InstanceToValidate;

            if (instance != null) return base.Validate(context);

            var errors = BusinessErrorsWhenModelIsNull() ?? Enumerable.Empty<BusinessError>();

            var validationResult =
                new ValidationResult(errors.Select(x => new ValidationFailure(typeof(T).Name, x.Description)
                {
                    ErrorCode = x.Code.ToString()
                }));

            return validationResult;
        }

        public virtual IEnumerable<BusinessError> BusinessErrorsWhenModelIsNull()
        {
            return Enumerable.Empty<BusinessError>();
        }

        #endregion

        #region Private Methods

        Result Map(ValidationResult validationResult)
        {
            if (validationResult.IsValid) return Result.Success;

            var result = new Result();

            foreach (var validationFailure in validationResult.Errors)
            {
                var error = new BusinessError
                {
                    Code = validationFailure.ErrorCode,
                    Description = validationFailure.ErrorMessage
                };

                switch (validationFailure.Severity)
                {
                    case Severity.Error:
                    {
                        error.Severity = Contracts.BLL.Domain.BusinessRules.Severity.Error;
                        result.Errors.Add(error);
                        break;
                    }
                    case Severity.Info:
                    {
                        error.Severity = Contracts.BLL.Domain.BusinessRules.Severity.Info;
                        result.Info.Add(error);
                        break;
                    }
                    case Severity.Warning:
                    {
                        error.Severity = Contracts.BLL.Domain.BusinessRules.Severity.Warning;
                        result.Warnings.Add(error);
                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException(validationFailure.Severity.ToString("G"));
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
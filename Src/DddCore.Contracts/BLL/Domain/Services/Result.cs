using DddCore.Contracts.BLL.Domain.BusinessRules;
using System.Collections.Generic;
using System.Linq;

namespace DddCore.Contracts.BLL.Domain.Services
{
    /// <summary>
    /// Instead of throwing exceptions that can be processed use operation result model to return result of operation.
    /// Commonly used in validation methods. See IBusinessRulesValidator example. 
    /// // If your method does a validation and returns result use tupple to return result: (MyModel output, OperationResult result).
    /// </summary>
    public class Result
    {
        /// <summary>
        /// False if has at least 1 Error. True if has warnings or info but no Errors.
        /// </summary>
        public bool IsSuccess => !IsFailure;
        public bool IsFailure => Errors.Any();

        public ICollection<BusinessError> Errors { get; set; } = new List<BusinessError>();
        public ICollection<BusinessError> Warnings { get; set; } = new List<BusinessError>();
        public ICollection<BusinessError> Info { get; set; } = new List<BusinessError>();

        public static Result Success = new Result();

        public static Result Fail(string code, string description)
        {
            return new Result {Errors = {BusinessError.Create(code, description)}};
        }

        public static Result Fail(params BusinessError[] errors)
        {
            var operationResult = new Result();

            foreach (var error in errors)
            {
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }

        public static Result Fail(IEnumerable<BusinessError> errors)
        {
            return Fail(errors.ToArray());
        }

        public static Result<T> Fail<T>(params BusinessError[] errors)
        {
            var operationResult = new Result<T>();

            foreach (var error in errors)
            {
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }

        public static Result<T> Fail<T>(IEnumerable<BusinessError> errors)
        {
            return Fail<T>(errors.ToArray());
        }

        public static Result<T> Fail<T>(string code, string description)
        {
            return new Result<T> {Errors = {BusinessError.Create(code, description)}};
        }

        public static Result<T> Ok<T>(T data)
        {
            return new Result<T> {Data = data};
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
}

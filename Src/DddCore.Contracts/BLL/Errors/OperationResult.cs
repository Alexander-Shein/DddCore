using System.Collections.Generic;
using System.Linq;

namespace DddCore.Contracts.BLL.Errors
{
    /// <summary>
    /// Instead of throwing exceptions that can be processed use operation result model to return result of operation.
    /// Commonly used in validation methods. See IBusinessRulesValidator example. 
    /// // If your method does a validation and returns result use tupple to return result: (MyModel output, OperationResult result).
    /// </summary>
    public class OperationResult
    {
        public static OperationResult SucceedResult = new OperationResult();

        public bool IsSucceed => !IsNotSucceed;

        public bool IsNotSucceed => Errors.Any();

        public ICollection<Error> Errors { get; } = new List<Error>();

        public static OperationResult FailedResult(int code, string description)
        {
            return new OperationResult
            {
                Errors =
                {
                    new Error
                    {
                        Code = code,
                        Description = description
                    }
                }
            };
        }
    }
}
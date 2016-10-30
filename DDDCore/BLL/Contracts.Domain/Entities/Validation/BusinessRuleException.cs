using System;

namespace Contracts.Domain.Entities.Validation
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}
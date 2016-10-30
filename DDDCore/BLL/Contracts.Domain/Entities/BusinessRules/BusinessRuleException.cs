using System;

namespace Contracts.Domain.Entities.BusinessRules
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}
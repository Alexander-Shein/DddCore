using System;

namespace Contracts.Domain.Entities.BusinessRules
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }

    public class BrokenRule
    {
        public BrokenRule(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
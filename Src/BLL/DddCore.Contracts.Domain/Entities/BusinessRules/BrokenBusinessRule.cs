using System;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public class BrokenBusinessRule : Exception
    {
        public BrokenBusinessRule(string name, string description) : base($"{name}: {description}.")
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
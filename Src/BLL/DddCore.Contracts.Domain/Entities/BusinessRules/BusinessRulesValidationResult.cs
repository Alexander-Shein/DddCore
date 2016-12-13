using System.Collections.Generic;
using System.Linq;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public class BusinessRulesValidationResult
    {
        public bool IsValid => !IsNotValid;

        public bool IsNotValid => Errors.Any();

        public ICollection<BrokenBusinessRule> Errors { get; } = new List<BrokenBusinessRule>();
    }
}
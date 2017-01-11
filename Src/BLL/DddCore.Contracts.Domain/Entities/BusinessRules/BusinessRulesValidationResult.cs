using System.Collections.Generic;
using System.Linq;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public class BusinessRulesValidationResult
    {
        public bool IsValid => !IsNotValid;

        public bool IsNotValid => BrokenBusinessRules.Any();

        public ICollection<BrokenBusinessRule> BrokenBusinessRules { get; } = new List<BrokenBusinessRule>();
    }
}
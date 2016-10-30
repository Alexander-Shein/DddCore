using System.Collections.Generic;
using System.Linq;

namespace Contracts.Domain.Entities.BusinessRules
{
    public class BusinessRulesValidationResult
    {
        public bool IsValid => !IsNotValid;

        public bool IsNotValid => Errors.Any();

        public ICollection<BusinessRuleException> Errors { get; set; } = new List<BusinessRuleException>();
    }
}
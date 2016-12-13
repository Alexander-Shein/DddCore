namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public class BrokenBusinessRule
    {
        public BrokenBusinessRule(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
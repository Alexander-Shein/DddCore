namespace Domain.ValueObjects
{
    public class FullName : ValueObject<FullName>
    {
        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public FullName(FullName fullName) : this(fullName.FirstName, fullName.LastName) { }

        internal FullName() { }

        public string FirstName { get; }
        public string LastName { get; }
    }
}

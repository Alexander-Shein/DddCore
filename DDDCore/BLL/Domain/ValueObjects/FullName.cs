using System;

namespace Domain.ValueObjects
{
    public class FullName : ValueObject<FullName>
    {
        public FullName(string firstName, string lastName, string middleName = "")
        {
            FirstName = firstName ?? String.Empty;
            LastName = lastName ?? String.Empty;
            MiddleName = middleName ?? String.Empty;
        }

        internal FullName() { }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string MiddleName { get; protected set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

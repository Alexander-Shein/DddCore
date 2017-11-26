using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DddCore.BLL.Domain.ValueObjects.Persons
{
    public class Email : ValueObjectBase<Email>
    {
        //Email regex from http://hexillion.com/samples/#Regex
        private static readonly Regex EmailRegex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$");

        private readonly string _emailString;

        public Email(string email)
        {
            if (!IsValidEmailString(email)) throw new ArgumentException(nameof(email));
            _emailString = email.ToLower();
        }

        public static bool IsValidEmailString(string email)
        {
            if (String.IsNullOrWhiteSpace(email)) return false;
            return EmailRegex.IsMatch(email);
        }

        public static bool TryCreate(string email, out Email emailOut)
        {
            emailOut = null;
            if (!IsValidEmailString(email)) return false;
            emailOut = new Email(email);
            return true;
        }

        public override string ToString()
        {
            return _emailString;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return _emailString;
        }
    }
}
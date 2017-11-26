using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace DddCore.BLL.Domain.ValueObjects.Persons
{
    public class PhoneNumber : ValueObjectBase<PhoneNumber>
    {
        private const string Numbers = @"\D";
        private const char Zero = '0';
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

        private readonly PhoneNumbers.PhoneNumber _phone;

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return ToString();
        }

        public PhoneNumber(string phoneNumber)
        {
            _phone = PhoneNumberUtil.Parse(phoneNumber, String.Empty);
        }

        public static bool IsValidPhoneNumberString(string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber)) return false;
            string phoneCorrect = Regex.Replace(phoneNumber, Numbers, string.Empty).TrimStart(Zero);

            try
            {
                var numberLib = PhoneNumberUtil.Parse(phoneCorrect, string.Empty);
                return PhoneNumberUtil.IsValidNumber(numberLib);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryCreate(string phoneNumber, out PhoneNumber phoneNumberInstance)
        {
            if (IsValidPhoneNumberString(phoneNumber))
            {
                phoneNumberInstance = new PhoneNumber(phoneNumber);
                return true;
            }

            phoneNumberInstance = null;
            return false;
        }

        public override string ToString()
        {
            return PhoneNumberUtil.Format(_phone, PhoneNumberFormat.INTERNATIONAL);
        }

        public string ToString(string format)
        {
            if (String.IsNullOrWhiteSpace(format)) return ToString();
            if (!Enum.TryParse(format, out PhoneNumberFormat phoneNumberFormat)) return ToString();

            return PhoneNumberUtil.Format(_phone, phoneNumberFormat);
        }
    }
}
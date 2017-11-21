using System;
using System.Collections.Generic;
using DddCore.BLL.Domain.ValueObjects;
using PhoneNumbers;

namespace DddCore.BLL.Domain
{
    public class PhoneNumber : ValueObjectBase<PhoneNumber>
    {
        private PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();

        private string _phone;

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return _phone;
        }

        public PhoneNumber(string phoneNumber)
        {
            var ol = _phoneNumberUtil.Parse(phoneNumber, String.Empty);
        }

        public string E164
        {
            get { return _phone; }
        }
    }
}
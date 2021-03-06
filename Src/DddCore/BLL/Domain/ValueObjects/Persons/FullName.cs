﻿using System;
using System.Collections.Generic;

namespace DddCore.BLL.Domain.ValueObjects.Persons
{
    public class FullName : ValueObjectBase<FullName>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            throw new System.NotImplementedException();
        }

        public static bool TryParse(string name, out FullName fullName)
        {
            fullName = null;

            if (String.IsNullOrWhiteSpace(name)) return false;

            return true;
        }
    }
}
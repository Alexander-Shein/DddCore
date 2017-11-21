using System.Collections.Generic;
using DddCore.BLL.Domain.ValueObjects;

namespace DddCore.BLL.Domain
{
    public class Email : ValueObjectBase<Email>
    {
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            throw new System.NotImplementedException();
        }
    }
}
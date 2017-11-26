using System.Collections.Generic;

namespace DddCore.BLL.Domain.ValueObjects.Locations
{
    public class Address : ValueObjectBase<Address>
    {
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            throw new System.NotImplementedException();
        }
    }
}

using System.Collections.Generic;

namespace DddCore.BLL.Domain.ValueObjects.DateTimeFrames
{
    public class Period : ValueObjectBase<Period>
    {
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            throw new System.NotImplementedException();
        }
    }
}